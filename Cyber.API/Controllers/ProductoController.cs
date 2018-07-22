using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Cyber.API.Models;
using Cyber.API.Models.DTO;

namespace Cyber.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Product")]
    public class ProductoController : ApiController
    {
        private CyberModel db = new CyberModel();



        // GET: api/Producto
        [HttpGet]
        [Route("getProducts")]
        public List<ProductoDTO> GetProductos()
        {
            return db.Productos.Select(s => new ProductoDTO {
                idProducto = s.idProducto,
                nombre = s.nombre,
                cantidad = s.cantidad,
                descripcion = s.descripcion,
                imagen = s.imagen,
                linkFabricante = s.linkFabricante,
                precio = s.precio,
                idCatProducto = s.idCatProducto
            }).ToList();
        }

        // GET: api/Producto/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> GetProducto(int id)
        {
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Producto/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.idProducto)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Producto
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(producto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = producto.idProducto }, producto);
        }

        // DELETE: api/Producto/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> DeleteProducto(int id)
        {
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Productos.Remove(producto);
            await db.SaveChangesAsync();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Productos.Count(e => e.idProducto == id) > 0;
        }
    }
}