using MvcApiPersonajesJavierPantoja.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesJavierPantoja.Services
{
    public class ServiceApiPersonajes
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiCrudPersonajes");
        }

        //METODO PARA COMUNICARSE CON LA API
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        //METODO PARA SACAR TODOS LOS PERSONAJES
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        //METODO PARA SACAR LOS DETALLES
        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/personajes/" + id;
            Personaje per = await this.CallApiAsync<Personaje>(request);
            return per;
        }

        //METODO PARA INSERTAR UN NUEVO PERSONAJE
        public async Task InsertPersonajes(int idPersonaje, string nombre, string imagen, int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //TENEMOS QUE ENVIAR UN OBJETO JSON
                //NOS CREAMOS UN OBJETO DE LA CLASE PERSONAJE
                Personaje per = new Personaje();
                per.IdPersonaje = idPersonaje;
                per.Nombre = nombre;
                per.Imagen = imagen;
                per.IdSerie = idSerie;
               
                //CONVERTIMOS EL OBJETO DEPARTAMENTO A JSON
                string json = JsonConvert.SerializeObject(per);
                
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        //METODO PARA MODIFICAR PERSONAJES
        public async Task UpdatePersonajes(int idPersonaje, string nombre, string imagen, int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje per = new Personaje
                {
                    IdPersonaje = idPersonaje,
                    Nombre = nombre,
                    Imagen = imagen,
                    IdSerie = idSerie
                };

                string json = JsonConvert.SerializeObject(per);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        //METODO PARA ELIMINAR PERSONAJE
        public async Task DeletePersonajesAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //NO NECESITA EL HEADER PORQUE NO DEVUELVE NADA
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
               
            }

        }

    }
}
