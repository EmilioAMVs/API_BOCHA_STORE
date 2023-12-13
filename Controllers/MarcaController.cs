using API_BOCHA_STORE.Data;
using API_BOCHA_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BOCHA_STORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public MarcaController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        // GET: api/<MarcaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Marca> marcas = await _dbContext.Marca.ToListAsync();

            return marcas == null ? BadRequest("Hubo un error al consultar las marcas") : Ok(marcas);
        }

        // GET api/<MarcaController>/5
        [HttpGet("{idMarca}")]
        public async Task<IActionResult> Get(int idMarca)
        {
            Marca marcaFounded = await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == idMarca);
            return marcaFounded == null ? BadRequest() : Ok(marcaFounded);
        }

        // GET api/<MarcaController>/5
        [HttpGet("/ProductosDeUnaMarca/{idMarca}")]
        public async Task<IActionResult> GetProductosDeUnaMarca(int idMarca)
        {
            // Buscar la marca
            Marca marca = await _dbContext.Marca
                .SingleOrDefaultAsync(m => m.idMarca== idMarca);

            // Si la marca no existe, devuelve BadRequest
            if (marca == null)
            {
                return BadRequest("La marca no existe");
            }

            // Buscar los productos que tienen esta marca
            List<Producto> productos = await _dbContext.Producto
                .Where(m => m.idMarca== idMarca)
                .ToListAsync();
            // Si todo está bien, devolver la lista de productos
            return productos.Count > 0 ? Ok(productos) : BadRequest("No existen productos asociados a la marca");
        }

        // Eliminar marca de producto
        [HttpDelete("/EliminarMarca/{idProducto}")]
        public async Task<IActionResult> EliminarMarca(int idProducto)
        {
            // Buscar el producto
            Producto producto = await _dbContext.Producto
                .SingleOrDefaultAsync(m => m.idProducto == idProducto);

            // Si el producto no existe, devuelve BadRequest
            if (producto == null)
            {
                return BadRequest("El producto al que desea elminar la marca no existe");
            }

            // Comprobar si el producto tiene una marca
            if (producto.idMarca == null || producto.idMarca== -1)
            {
                return BadRequest("El producto no tiene una marca para eliminar!");
            }

            // Busca la marca del producto
            Marca marca = await _dbContext.Marca
                .SingleOrDefaultAsync(m => m.idMarca == producto.idMarca);

            // Si la marca no existe, devolver BadRequest
            if (producto == null)
            {
                return BadRequest("La marcar a elminar no existe!");
            }

            // Elimina la marca del producto
            producto.idMarca = -1;

            // Guarda los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            // Si todo está bien, devolver Ok
            return Ok($"La marca del{producto.nombreProducto} ha sido eliminda con éxito");
        }


        // POST api/<MarcaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Marca newMarca)
        {
            Marca marcaFounded = await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == newMarca.idMarca);

            if (newMarca == null || marcaFounded != null)
            {
                return BadRequest("La nueva marcac tiene errores o ya existe la marca a registrar");
            }

            await _dbContext.Marca.AddAsync(newMarca);
            await _dbContext.SaveChangesAsync();

            return Ok(newMarca);
        }

        // PUT api/<MarcaController>/5
        [HttpPut("{idMarca}")]
        public async Task<IActionResult> Put(int idMarca, [FromBody] Marca newMarca)
        {
            Marca marcaToReplace= await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == idMarca);

            if (newMarca == null || marcaToReplace== null)
            {
                return BadRequest("La nueva marca tiene errores o no existe la marca a reemplazar!");
            }

            marcaToReplace.nombreMarca= newMarca.nombreMarca== null ? marcaToReplace.nombreMarca: newMarca.nombreMarca;
            await _dbContext.SaveChangesAsync();

            return Ok(marcaToReplace);
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("{idMarca}")]
        public async Task<IActionResult> Delete(int idMarca)
        {
            Marca marcaToDelete = await _dbContext.Marca.FirstOrDefaultAsync(data => data.idMarca == idMarca);

            if (marcaToDelete == null)
            {
                return BadRequest("No se ha encontrado la marca a borrar");
            }

            _dbContext.Remove(marcaToDelete);
            await _dbContext.SaveChangesAsync();

            return Ok("La marca ha sido eliminado correctamente!");
        }
    }
}
