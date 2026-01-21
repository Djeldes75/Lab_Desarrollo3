using log4net;
using PizzaraWebService.Data;
using PizzaraWebService.Models;
using System;
using System.Web.Http;
using WebGrease;
using LogManager = log4net.LogManager;

[RoutePrefix("api/pizarra")]
public class PizarraController : ApiController
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PizarraController));
    private readonly PizarraRepository _repo = new PizarraRepository();

    [HttpPost]
    [Route("evento")]
    public IHttpActionResult PostEvento([FromBody] EventoDto evento)
    {
        if (evento == null)
        {
            log.Warn("Request vacío recibido en /api/pizarra/evento");
            return BadRequest("Payload vacío");
        }

        if (string.IsNullOrWhiteSpace(evento.IdJuego))
        {
            return BadRequest("IdJuego es obligatorio.");
        }

        try
        {
            log.Info($"Evento recibido: IdJuego={evento.IdJuego} Inning={evento.Inning} Carrera={evento.Carrera} Pelotero={evento.Pelotero}");

            // Insertar evento
            var insertResult = _repo.InsertEvento(evento); // implementa en repo para llamar al SP
            // Upsert pizarra (actualizar estado del juego)
            var upsertResult = _repo.UpsertJuego(evento.IdJuego, evento.Abre, evento.Cierra, null, null, evento.Inning, null);

            var resp = new RespuestaDto
            {
                Estado = "OK",
                Mensaje = "Evento registrado y pizarra actualizada",
                Data = new { Insert = insertResult, Upsert = upsertResult }
            };

            log.Info("Evento procesado correctamente.");
            return Ok(resp);
        }
        catch (Exception ex)
        {
            log.Error("Error procesando evento: " + ex.ToString());
            return InternalServerError(ex);
        }
    }
}
