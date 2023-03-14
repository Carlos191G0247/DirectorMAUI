using DirectorMAUI.Models;
using DirectorMAUI.Services;
using DirectorMAUI.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DirectorMAUI.ViewModel
{
    public class DirectorViewModel : INotifyPropertyChanged
    {
        readonly DirectorService server = new();
        readonly UsuariosService usuarioserver = new();
        readonly AsignarService asignarserver = new();
        public Command EliminarDocenteCommand { get; set; }
        public Command EliminarUsuarioCommand { get; set; }
        public Command IniciarSesionCommand { get; set; }
        public Command EditarDocenteCommand { get; set; }
        public Command InsertUsuarioCommand { get; set; }
        public Command EditarUsuarioCommand { get; set; }

        public Command AsignarGrupoCommand { get; set; }



        public Command InsertDocentesCommand { get; set; }
        public Command VerInsertDocentesCommand { get; set; }
        public Command VerEditarDocentesCommand { get; set; }
        public Command VerEditarUsuarioCommand { get; set; }
        public Command VerInsertarUsuarioCommand { get; set; }
        public Command VerAsignarDocenteGruposCommand { get; set; }



        public Docente docente { get; set; } = new Docente();
        public Usuario usuario { get; set; } = new Usuario();
        public DocenteGrupo docgrupo { get; set; } = new DocenteGrupo();
        public ObservableCollection<Docente> DocenteList { get; set; } = new ObservableCollection<Docente>();
        public ObservableCollection<Usuario> UsuarioList { get; set; } = new ObservableCollection<Usuario>();
        /// <summary>
        /// /
        /// </summary>
        /// 
        public ObservableCollection<Grupo> Grupolist { get; set; } = new ObservableCollection<Grupo>();
        public ObservableCollection<Asignatura> AsignaturaList { get; set; } = new ObservableCollection<Asignatura>();
        public ObservableCollection<Periodo> PeriodoList { get; set; } = new ObservableCollection<Periodo>();
        public ObservableCollection<DocenteGrupo> DocenteGrupoList { get; set; } = new ObservableCollection<DocenteGrupo>();
        public ObservableCollection<DocenteAsignatura> DocenteAsignaturaList { get; set; } = new ObservableCollection<DocenteAsignatura>();

        /// <summary>
        /// 
        /// </summary>
   






        void Actualizar(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        InsertarDocenteView docentevista;

        public event PropertyChangedEventHandler PropertyChanged;

        public DirectorViewModel()
        {
            EliminarDocenteCommand = new Command<Docente>(EliminarDocente);
            InsertDocentesCommand = new Command(InsertarDocente);
            IniciarSesionCommand = new Command(Login);
            EditarDocenteCommand = new Command(EditarDocente);
            EliminarUsuarioCommand = new Command<Usuario>(EliminarUsuario);
            InsertUsuarioCommand = new Command(InsertUsuario);
            EditarUsuarioCommand = new Command(EditarUsuario);
            VerAsignarDocenteGruposCommand = new Command(VerAsignarDocenteGrupos);

            AsignarGrupoCommand = new Command(AsignarDocenteGrupo);



            VerInsertarUsuarioCommand = new Command(VerInsertarUsuario);
            VerEditarDocentesCommand = new Command<Docente>(VerEditarDocente);
            VerEditarUsuarioCommand = new Command<Usuario>(VerEditarUsuario);

            VerInsertDocentesCommand = new Command(VerInsertarDocente);
            cargarDocentes();
            CargarUsuario();

            CargarGrupo();
            CargarAsignatura();
            CargarPeriodo();
            CargarDocenteGrupos();
            CargarDocenteAsignatura();
        }
        public async void Login()
        {
            UsuarioList.Add(usuario);
            Actualizar(nameof(UsuarioList));
            var datos = await server.LoginDir(usuario);
            App.Current.MainPage = new NavigationPage(new AppShell());


        }

        async void cargarDocentes()
        {
            DocenteList.Clear();
            var datos = await server.GetGrupoDocentes();
            datos.ForEach(x => DocenteList.Add(x));
            Actualizar(nameof(DocenteList));

        }
        async void CargarUsuario()
        {
            UsuarioList.Clear();
            var datos = await usuarioserver.GetUsuarios();
            datos.ForEach(x => UsuarioList.Add(x));
            Actualizar(nameof(UsuarioList));

        }
        /// <summary>
        /// ///////////////////
        /// </summary>
        /// PASO FINAL
        
        async void CargarGrupo()
        {
            Grupolist.Clear();
            var datos = await asignarserver.GetGrupo();
            datos.ForEach(x => Grupolist.Add(x));
            Actualizar(nameof(Grupolist));
        }
        async void CargarAsignatura()
        {
            AsignaturaList.Clear();
            var datos = await asignarserver.GetAsignatura();
            datos.ForEach(x => AsignaturaList.Add(x));
            Actualizar(nameof(AsignaturaList));
        }
        async void CargarPeriodo()
        {
            PeriodoList.Clear();
            var datos = await asignarserver.GetPeriodo();
            datos.ForEach(x => PeriodoList.Add(x));
            Actualizar(nameof(PeriodoList));
        }
        async void CargarDocenteGrupos()
        {
            DocenteGrupoList.Clear();
            var datos = await asignarserver.GetDocenteGrupo();
            datos.ForEach(x => DocenteGrupoList.Add(x));
            Actualizar(nameof(DocenteAsignaturaList));
        }
        async void CargarDocenteAsignatura()
        {
            DocenteAsignaturaList.Clear();
            var datos = await asignarserver.GetDocenteAsignatura();
            datos.ForEach(x => DocenteAsignaturaList.Add(x));
            Actualizar(nameof(DocenteAsignaturaList));
        }
        AsignarDGView asgdgView;
        private async void VerAsignarDocenteGrupos()
        {
            docgrupo = new DocenteGrupo();
            asgdgView = new AsignarDGView() { BindingContext = this };

            await Application.Current.MainPage.Navigation.PushAsync(asgdgView);
        }
        public Docente doc { get; set; }
        public Grupo grup { get; set; }
        public Periodo peri { get; set; }
        public async void AsignarDocenteGrupo()
        {
            docgrupo.IdDocente = doc.Id;
            docgrupo.IdGrupo = grup.Id;
            docgrupo.IdPeriodo = peri.Id;
            DocenteGrupoList.Add(docgrupo);
            Actualizar(nameof(DocenteGrupoList));
            await asignarserver.InsertAsignarGrupo(docgrupo);
            await Application.Current.MainPage.Navigation.PopAsync();
            


        }

        /// <summary>
        /// //
        /// </summary>

        public Usuario SUS { get; set; }
        public async void InsertarDocente()
        {
            docente.IdUsuario = SUS.Id;
            DocenteList.Add(docente);
            Actualizar(nameof(DocenteList));
            await server.InsertDocente(docente);
            await Application.Current.MainPage.Navigation.PopAsync();
            cargarDocentes();


        }
        public async void InsertUsuario()
        {
            usuario.Rol = 2;
            UsuarioList.Add(usuario);
            Actualizar(nameof(UsuarioList));
            await usuarioserver.InsertUsua(usuario);
            await Application.Current.MainPage.Navigation.PopAsync();
            CargarUsuario();

        }
        EditarDocenteView views;
        private async void VerEditarDocente(Docente docentess)
        {
            docente = docentess;
            docente.Nombre = docentess.Nombre;
            docente.ApellidoMaterno = docentess.ApellidoMaterno;
            docente.ApellidoPaterno = docentess.ApellidoPaterno;
            docente.Correo = docentess.Correo;
            docente.Telefono = docentess.Telefono;
            docente.Edad = docentess.Edad;
            docente.TipoDocente = docentess.TipoDocente;
            docente.IdUsuario = docentess.IdUsuario;

            views = new EditarDocenteView() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(views);
        }
        VerEditarUsuariosView vereditview;
        private async void VerEditarUsuario(Usuario usuariosss)
        {
            usuario = usuariosss;
            usuario.Usuario1 = usuariosss.Usuario1;
            usuario.Contraseña = usuariosss.Contraseña;
            usuario.Rol = usuariosss.Rol;

            vereditview = new VerEditarUsuariosView() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(vereditview);
        }
        public async void EditarDocente()
        {
            await server.UpdateDocente(docente);
            await Application.Current.MainPage.Navigation.PopAsync();
            cargarDocentes();

        }
        public async void EditarUsuario()
        {
            await usuarioserver.UpdateUsuario(usuario);
            await Application.Current.MainPage.Navigation.PopAsync();
            CargarUsuario();

        }
        InsertarDocenteView view;
        private async void VerInsertarDocente()
        {
            docente = new Docente();
            view = new InsertarDocenteView() { BindingContext = this };

            await Application.Current.MainPage.Navigation.PushAsync(view);
        }
        InsertarUsuarioView viewusu;
        private async void VerInsertarUsuario()

        {
            usuario = new Usuario();
            viewusu = new InsertarUsuarioView() { BindingContext = this };

            await Application.Current.MainPage.Navigation.PushAsync(viewusu);
        }
        public async void EliminarDocente(Docente d)
        {

            DocenteList.Remove(d);
            await server.DeleteDocente(d);
            Actualizar(nameof(DocenteList));
            cargarDocentes();
        }
        public async void EliminarUsuario(Usuario u)
        {

            UsuarioList.Remove(u);
            await usuarioserver.DeleteUsuario(u);
            Actualizar(nameof(UsuarioList));
            CargarUsuario();
        }

    }


}

