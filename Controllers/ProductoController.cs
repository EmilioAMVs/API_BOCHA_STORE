﻿using API_BOCHA_STORE.Data;
using API_BOCHA_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BOCHA_STORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;

        public ProductoController(ApplicationDBContext dbContext)
        {
            // Inyección de dependencias, inyecto la dependencia de la DB
            _dbContext = dbContext;

        }

        // GET: api/<MiembroController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Producto> miembros = await _dbContext.Producto.ToListAsync();

            return Ok(miembros);

        }

        // GET api/<MiembroController>/5
        [HttpGet("{idMiembro}")]
        public async Task<IActionResult> Get(int idMiembro)
        {

            Producto miembroFounded = await _dbContext.Producto.FirstOrDefaultAsync( data => data.idProducto == idMiembro);

            return miembroFounded == null ? BadRequest() : Ok(miembroFounded);

        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto newProducto)
        {

            // En caso de que el framework no valide que ya exista un ID que ya existe debemos validar nosotros
            Producto productoFounded = await _dbContext.Producto.FirstOrDefaultAsync(x => x.idProducto == newProducto.idProducto);

            if (newProducto == null || productoFounded != null)
            {

                return BadRequest("El nuevo producto tiene errores o ya existe el producto a registrar!");

            }

            await _dbContext.Producto.AddAsync(newProducto);
            await _dbContext.SaveChangesAsync();

            return Ok(newProducto);

        }

        // PUT api/<ProductoController>/5
        [HttpPut("{idProductoToReplace}")]
        public async Task<IActionResult> Put(int idProductoToReplace, [FromBody] Producto newProducto)
        {

            Producto productoToReplace = await _dbContext.Producto.FirstOrDefaultAsync(data => data.idProducto == idProductoToReplace);

            if (newProducto== null || productoToReplace == null)
            {

                return BadRequest("El nuevo producto tiene errores o no existe el producto a reemplazar!");

            }

            productoToReplace.nombreProducto= newProducto.nombreProducto== null ? productoToReplace.nombreProducto: newProducto.nombreProducto;
            productoToReplace.idProovedor = newProducto.idProovedor == null ? productoToReplace.idProovedor : newProducto.idProovedor; ;
            productoToReplace.descripcionProducto= newProducto.descripcionProducto== null ? productoToReplace.descripcionProducto: newProducto.descripcionProducto;
            productoToReplace.precio = newProducto.precio == null ? productoToReplace.precio : newProducto.precio;
            productoToReplace.stock = newProducto.stock == null ? productoToReplace.stock : newProducto.stock;  
            productoToReplace.fechaCreacion= newProducto.fechaCreacion== null ? productoToReplace.fechaCreacion : newProducto.fechaCreacion;

            await _dbContext.SaveChangesAsync();

            return Ok(productoToReplace);

        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{idProductoToDelete}")]
        public async Task<IActionResult> Delete(int idProductoToDelete)
        {

            Producto productoToDelete = await _dbContext.Producto.FirstOrDefaultAsync( data => data.idProducto== idProductoToDelete);

            if (productoToDelete == null ) 
            { 
            
                return BadRequest();

            }

            _dbContext.Remove(productoToDelete);

            await _dbContext.SaveChangesAsync();

            return Ok("El producto ha sido eliminado correctamente!");

        }
    }
}
