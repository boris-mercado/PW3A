using AppBlazor.Entities;
using System.Net.Http.Json;

namespace AppBlazor.Client.Services
{
    public class AutorService
    {
        private readonly HttpClient http;
        public AutorService(HttpClient _http)
        {
            http = _http;
        }

        public async Task<List<AutorCLS>> listarAutores()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<AutorCLS>>("api/Autor");
                if (response == null)
                {
                    return new List<AutorCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new List<AutorCLS>();
            }
        }
    }
}
