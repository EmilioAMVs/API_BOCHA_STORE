using API_BOCHA_STORE.Data;
using API_BOCHA_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BOCHA_STORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProovedorController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public ProovedorController (ApplicationDBContext dBContext)
        {

            _dbContext = dBContext;

        }

        // GET: api/<ProovedorController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Proovedor> proovedores = await _dbContext.Proovedor.ToListAsync();

            return proovedores == null ? BadRequest("Hubo un error al consultar los proovedores!") : Ok(proovedores);

        }

        // GET api/<ProovedorController>/5
        [HttpGet("{idProovedor}")]
        public async Task<IActionResult> Get(int idProovedor)
        {

            Proovedor proovedorFounded = await _dbContext.Proovedor.FirstOrDefaultAsync(data => data.idProovedor == idProovedor);

            return proovedorFounded== null ? BadRequest() : Ok(proovedorFounded);

        }

        // GET api/<ProovedorController>/5
        [HttpGet("/ProductosDeUnProovedor/{idProovedor}")]
        public async Task<IActionResult> GetProductosDeUnProovedor(int idProovedor)
        {
            // Buscar el proovedor
            Proovedor proovedor = await _dbContext.Proovedor
                .SingleOrDefaultAsync(m => m.idProovedor == idProovedor);

            // Si el proovedor no existe, devolver BadRequest
            if (proovedor == null)
            {
                return BadRequest("El proovedor que consultó no existe!");
            }

            // Buscar los productos que tienen este proovedor
            List<Producto> productos= await _dbContext.Producto
                .Where(m => m.idProovedor== idProovedor)
                .ToListAsync();

            // Si todo está bien, devolver la lista de productos
            return productos.Count > 0 ? Ok(productos) : BadRequest("No existen productos asociados al proovedor consultado");
        }


        // Renovar Contrato
        [HttpGet("/RenovarContrato/{idProducto}")]
        public async Task<IActionResult> RenovarContrato(int idProducto)
        {
            // Buscar el producto
            Producto producto = await _dbContext.Producto
                .SingleOrDefaultAsync(data => data.idProducto == idProducto);

            // Si el producto no existe, devolver BadRequest
            if (producto == null)
            {
                return BadRequest("El producto para renovar el contrato no existe!");
            }

            // Buscar el proovedor del producto
            Proovedor proovedor = await _dbContext.Proovedor
                .SingleOrDefaultAsync(data => data.idProovedor== producto.idProovedor);

            // Si el proovedor no existe, devolver BadRequest
            if (proovedor== null)
            {
                return BadRequest($"El proovedor del producto{producto.nombreProducto} no existe");
            }

            // Si todo está bien, devolver Ok
            return Ok($"El contrato del {producto.nombreProducto} ha sido renovado con éxito!");
        }

        // Eliminar proovedor
        [HttpDelete("/EliminarProovedor/{idProducto}")]
        public async Task<IActionResult> EliminarProovedor(int idProducto)
        {
            // Buscar el producto
            Producto producto= await _dbContext.Producto
                .SingleOrDefaultAsync(m => m.idProducto== idProducto);

            // Si el producto no existe, devuelve BadRequest
            if (producto == null)
            {
                return BadRequest("El producto al que desea elminar el proovedor no existe");
            }

            // Comprobar si el producto tiene una proovedor
            if (producto.idProovedor== null || producto.idProovedor== -1)
            {
                return BadRequest("El producto no tiene un proovedor para eliminar!");
            }

            // Busca el proovedor del producto
            Proovedor proovedor = await _dbContext.Proovedor
                .SingleOrDefaultAsync(m => m.idProovedor == producto.idProovedor);

            // Si el proovedor no existe, devolver BadRequest
            if (producto == null)
            {
                return BadRequest("El proovedor a elminar no existe!");
            }

            // Eliminar el proovedor del producto
            producto.idProovedor = -1;

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            // Si todo está bien, devolver Ok
            return Ok($"El contrato con el proovedor del{producto.nombreProducto} ha sido cancelado con éxito!");
        }



        // POST api/<ProovedorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Proovedor newProovedor)
        {

            // En caso de que el framework no valide que ya exista un ID que ya existe debemos validar nosotros
            Proovedor proovedorFounded = await _dbContext.Proovedor.FirstOrDefaultAsync(data => data.idProovedor== newProovedor.idProovedor);

            if (newProovedor== null || proovedorFounded != null)
            {

                return BadRequest("El nuevo proovedor tiene errores o ya existe el proovedor a registrar!");

            }

            await _dbContext.Proovedor.AddAsync(newProovedor);
            await _dbContext.SaveChangesAsync();

            return Ok(newProovedor);

        }

        // PUT api/<ProovedorController>/5
        [HttpPut("{idProovedor}")]
        public async Task<IActionResult> Put(int idProovedor, [FromBody] Proovedor newProovedor)
        {

            Proovedor proovedorToReplace = await _dbContext.Proovedor.FirstOrDefaultAsync(data => data.idProovedor == idProovedor);

            if (newProovedor == null || proovedorToReplace== null)
            {

                return BadRequest("El nuevo proovedor tiene errores o no existe el proovedor a reemplazar!");

            }

            proovedorToReplace.nombreProovedor= newProovedor.nombreProovedor== null ? proovedorToReplace.nombreProovedor: newProovedor.nombreProovedor;
            proovedorToReplace.precioImportacion = newProovedor.precioImportacion== null ? proovedorToReplace.precioImportacion : newProovedor.precioImportacion;
            proovedorToReplace.duracionContrato= newProovedor.duracionContrato== null ? proovedorToReplace.duracionContrato : newProovedor.duracionContrato;

            await _dbContext.SaveChangesAsync();

            return Ok(proovedorToReplace);

        }

        // DELETE api/<ProovedorController>/5
        [HttpDelete("{idProovedor}")]
        public async Task<IActionResult> Delete(int idProovedor)
        {

            Proovedor proovedorToDelete = await _dbContext.Proovedor.FirstOrDefaultAsync(data => data.idProovedor == idProovedor);

            if (proovedorToDelete == null)
            {

                return BadRequest("No se ha encontrado el proovedor a borrar");

            }

            _dbContext.Remove(proovedorToDelete);

            await _dbContext.SaveChangesAsync();

            return Ok("El proovedor ha sido eliminado correctamente!");

        }
    }
}
