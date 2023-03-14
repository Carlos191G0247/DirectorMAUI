using DirectorMAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DirectorMAUI.Services
{
    public class DirectorService
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://director2.sistemas19.com//")
        };

        public event Action<string> Error;
        void LanzarError(string mensaje)
        {
            Error?.Invoke(mensaje);
        }
        void LanzarErrorJson(string json)
        {
            string obj = JsonConvert.DeserializeObject<string>(json);
            if (obj != null)
            {
                Error?.Invoke(obj);
            }
        }

        public async Task<List<Docente>> GetGrupoDocentes()
        {
            List<Docente> listaDocente = new List<Docente>();
            var response = await cliente.GetAsync("api/Docente");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaDocente = JsonConvert.DeserializeObject<List<Docente>>(json);
            }

            if (listaDocente != null)
            {
                return listaDocente;
            }
            else
            {
                return new List<Docente>();
            }
        }

        public async Task<bool> LoginDir(Usuario usu)
        {
            var json = JsonConvert.SerializeObject(usu);
            var response = await cliente.PostAsync("api/Usuario/login/", new StringContent(json, Encoding.UTF8,
                "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
        }
        public async Task<bool> InsertDocente(Docente docente)
        {


            var json = JsonConvert.SerializeObject(docente);
            var response = await cliente.PostAsync("api/Docente", new StringContent(json, Encoding.UTF8,
                "application/json"));


            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateDocente(Docente docente)
        {
            var json = JsonConvert.SerializeObject(docente);
            var response = await cliente.PutAsync("api/Docente/", new StringContent(json, Encoding.UTF8,
                "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el registro docente");
            }
            return true;
        }
        public async Task<bool> DeleteDocente(Docente d)
        {
            var response = await cliente.DeleteAsync("api/Docente/" + d.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el Id del docente");
            }
            return true;
        }

    }
}
