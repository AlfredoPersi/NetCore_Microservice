using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibroService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<LibrosService> logger;

        public LibrosService(IHttpClientFactory httpClient,
                             ILogger<LibrosService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool Resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid libroId)
        {
            try
            {
                // obtengo el httpClient que agregue en el startup para obtener su URL
                var cliente = httpClient.CreateClient("Libros");

                var response = await cliente.GetAsync($"api/LibroMaterial/{libroId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var libro = JsonSerializer.Deserialize<LibroRemote>(content, options);

                    return (true, libro, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}

