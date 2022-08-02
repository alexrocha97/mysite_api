using Api.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtigoController : ControllerBase
    {
        [HttpGet("ListaDeArtigos")]
        public async Task<ActionResult<IEnumerable<Artigo>>> ListaDeArtigos()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
