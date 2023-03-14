using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Matricula { get; set; } = null!;

        public int? IdGrupo { get; set; }

        public virtual ICollection<AlumnoTutor> AlumnoTutor { get; } = new List<AlumnoTutor>();

        public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

        public virtual ICollection<DocenteAlumno> DocenteAlumno { get; } = new List<DocenteAlumno>();

        public virtual Grupo IdGrupoNavigation { get; set; }
    }
}
