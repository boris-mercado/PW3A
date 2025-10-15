using AppBlazor.Entities;
using System.Net.Http.Json;

namespace AppBlazor.Client.Services
{
    public class LibroService
    {
        private List<LibroListCLS> lista;
        private TipoLibroService tipolibroservice;
        private readonly HttpClient http;

        //para componente de serch
        public event Func<string, Task> OnSearch = delegate { return Task.CompletedTask; };
        public async Task notificarBusqueda(string titulolibro)
        {
            if(OnSearch != null) await OnSearch.Invoke(titulolibro);
        }
        //nueva lista del servicio de libros
        public LibroService(TipoLibroService _tipolibroservice, HttpClient _http)
        {
            http = _http;
            tipolibroservice = _tipolibroservice;
            lista = new List<LibroListCLS>();
        }

        public async Task<List<LibroListCLS>> ListarLibros()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<LibroListCLS>>("api/Libro");
                if (response == null)
                {
                    return new List<LibroListCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new List<LibroListCLS>();
            }
        }
        public async Task<string> eliminarLibro(int idlibro)
        {
            var response = await http.DeleteAsync($"api/Libro/{idlibro}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error: " + await response.Content.ReadAsStringAsync();
            }
        }
        public void guardarLibro(LibroFormCLS oLibroFormCLS)
        {
            if (oLibroFormCLS.idLibro == 0)
            {
                int idlibro = lista.Select(p => p.idlibro).Max() + 1;
                lista.Add(new LibroListCLS
                {
                    idlibro = idlibro,
                    titulo = oLibroFormCLS.titulo,
                    nombretipolibro = tipolibroservice.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro),
                    imagen = oLibroFormCLS.image,
                  //archivo
                    archivo = oLibroFormCLS.archivo,
                    nombrearchivo = oLibroFormCLS.nombrearchivo
                });
            }
            else
            {
                var obj = lista.Where(p => p.idlibro == oLibroFormCLS.idLibro).FirstOrDefault();
                if (obj != null)
                {
                    obj.titulo = oLibroFormCLS.titulo;
                    obj.nombretipolibro = tipolibroservice.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro);
                   obj.imagen = oLibroFormCLS.image;
                    //archivo
                    obj.archivo = oLibroFormCLS.archivo;
                    obj.nombrearchivo = oLibroFormCLS.nombrearchivo;
                }
            }
        }
        // archivo
        public async Task<LibroFormCLS> recuperaLibroPorId(int idlibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<LibroFormCLS>("api/Libro/"+idlibro);
                if (response == null)
                {
                    //return "";
                    return new LibroFormCLS();
                }
                else
                {
                    //return "";
                    return response;
                }
            }
            catch
            {
                //return "";
                return new LibroFormCLS();
            }
        }
        public async Task<string> recuperarNombrePorId(int idlibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<byte[]>("api/Libro/recuperarArchivo/" + idlibro);

                if (response == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToBase64String(response);
                }
            }
            catch
            {
                return "";
            }
        }
        //para la busqueda
        public async Task<List<LibroListCLS>> filtrarLibros(string nombretitulo)
        {
            List<LibroListCLS> l = await ListarLibros(); 
            if(nombretitulo == "")
            {
                return l;
            }
            else
            {
                List<LibroListCLS> listafiltrada = l.Where(p=> p.titulo.ToUpper().Contains(nombretitulo.ToUpper())).ToList();
                return listafiltrada;
            }
        }


    }
}
