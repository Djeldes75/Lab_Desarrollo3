using DenunciasService.Data;
using DenunciasService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DenunciasService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DenunciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DenunciasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint seguro para recepción de denuncias.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearDenuncia([FromBody] Funcionario data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                data.fechaingreso = DateTime.Now;
                data.estado = true;

                await _context.Funcionarios.AddAsync(data);

                await _context.SaveChangesAsync();

                return StatusCode(201, new
                {
                    mensaje = "Datos procesados y asegurados correctamente.",
                    id_referencia = data.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor." });
            }
        }
    }
}
