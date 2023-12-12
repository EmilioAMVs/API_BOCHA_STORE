using API_BOCHA_STORE.Data;
using API_BOCHA_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BOCHA_STORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public MarcaController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<MarcaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Marca> marcas = await _dbContext.Marca.ToListAsync();

            return marcas == null ? BadRequest() : Ok(marcas);

        }

        // GET api/<PagoController>/5
        [HttpGet("{idPago}")]
        public async Task<IActionResult> Get(int idMarca)
        {

            Marca marcaFounded = await _dbContext.Marca.SingleOrDefaultAsync(p => p.idMarca == idMarca);

            return marcaFounded == null ? NotFound("La marca se ha encontrado o no existe!") : Ok(marcaFounded);

        }

        // POST api/<PagoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Marca marca)
        {

            if (marca == null)
            {

                return BadRequest("La marca no se registró correctamente, por favor intente de nuevo!");

            }

            _dbContext.Marca.Add(marca);

            await _dbContext.SaveChangesAsync();

            return Ok(marca);

        }

        // PUT api/<MarcaController>/5
        [HttpPut("{idMarca}")]
        public async Task<IActionResult> Put(int idMarca, [FromBody] Marca newMarca)
        {

            Marca pagoToReplace = await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == idMarca);

            if ( idMarca == null || idMarca < 0 || pagoToReplace == null)
            {

                return BadRequest("La marca a reemplazar no existe o  tiene errores");

            }

            _dbContext.Entry(newMarca).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return Ok(newMarca);

        }

        // DELETE api/<PagoController>/5
        [HttpDelete("{idMarca}")]
        public async Task<IActionResult> Delete(int idMarca)
        {

            Marca marcaToDelete = await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == idMarca);

            if (marcaToDelete == null)
            {

                return NotFound("La marca a borrar no se ha encontrado");

            }

            _dbContext.Marca.Remove(marcaToDelete);

            await _dbContext.SaveChangesAsync();

            return Ok("La marca ha sido eliminado exitosamente!");

        }

        // GET: api/<MarcaController>/MarcasPorProducto/5
        [HttpGet("/MarcasPorProducto/{idMarca}")]
        public async Task<IActionResult> MarcasPorProducto(int idProducto)
        {

            List<Marca> pagos = await _dbContext.Marca.Where(p => p.idMarca== idProducto).ToListAsync();

            if (pagos == null || pagos.Count == 0)
            {
                return NotFound("No se encontraron marcas para este miembro!");
            }

            return Ok(pagos);

        }
    }
}

