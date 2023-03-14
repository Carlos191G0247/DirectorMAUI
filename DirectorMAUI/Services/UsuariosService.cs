using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectorMAUI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DirectorMAUI.Services
{
    public class UsuariosService
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://director2.sistemas19.com//")
        };
        void LanzarError(string mensaje)
        {
            Error?.Invoke(mensaje);
        }

        public event Action<string> Error;
        void LanzarErrorJson(string json)
        {
            string obj = JsonConvert.DeserializeObject<string>(json);
            if (obj != null)
            {
                Error?.Invoke(obj);
            }
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> listausu = new List<Usuario>();
            var response = await cliente.GetAsync("api/Usuario");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listausu = JsonConvert.DeserializeObject<List<Usuario>>(json);
            }

            if (listausu != null)
            {
                return listausu;
            }
            else
            {
                return new List<Usuario>();
            }
        }
        public async Task<bool> InsertUsua(Usuario u)
        {


            var json = JsonConvert.SerializeObject(u);
            var response = await cliente.PostAsync("api/Usuario", new StringContent(json, Encoding.UTF8,
                "application/json"));


            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateUsuario(Usuario u)
        {
            var json = JsonConvert.SerializeObject(u);
            var response = await cliente.PutAsync("api/Usuario/", new StringContent(json, Encoding.UTF8,
                "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el registro del Usuario");
            }
            return true;
        }
        public async Task<bool> DeleteUsuario(Usuario u)
        {
            var response = await cliente.DeleteAsync("api/Usuario/" + u.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el Id del Usuario");
            }
            return true;
        }



    }
}
