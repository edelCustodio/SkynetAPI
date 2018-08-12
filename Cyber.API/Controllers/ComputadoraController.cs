using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Cyber.API.Models;
using System.Data.SqlClient;
using Cyber.API.Models.DTO;

namespace Cyber.API.Controllers
{
    
    [RoutePrefix("api/Desktop")]
    public class ComputadoraController : ApiController
    {
        private CyberModel db = new CyberModel();

        // GET: api/Computadora
        public IQueryable<Computadora> GetComputadoras()
        {
            return db.Computadoras;
        }

        [HttpGet]
        [Route("getComputers")]
        public IEnumerable<ComputadoraDTO> GetComputadorasEnLinea()
        {
            return db.Computadoras.Where(w => w.enLinea == true).Select(s =>
                    new ComputadoraDTO
                    {
                        idComputadora = s.idComputadora,
                        nombre = s.nombre,
                        enLinea = s.enLinea,
                        costoRenta = s.costoRenta,
                        IP = s.IP
                    }).ToList();
        }

        [HttpPost]
        [Route("getDesktopByName")]
        public ComputadoraDTO GetDesktopByName([FromBody] ComputadoraDTO cdto)
        {
            var c = new ComputadoraDTO();

            var exists = db.Computadoras.Count(w => w.nombre == cdto.nombre) > 0;

            if (exists)
            {
                // obtener computadora
                var cmp = db.Computadoras.Where(w => w.nombre == cdto.nombre).FirstOrDefault();
                // actualizar IP
                cmp.IP = cdto.IP;
                // actualizar en base de datos
                db.Entry(cmp).State = EntityState.Modified;
                db.SaveChanges();

                // llenar objeto para retornar
                c.idComputadora = cmp.idComputadora;
                c.nombre = cmp.nombre;
                c.enLinea = cmp.enLinea;
                c.costoRenta = cmp.costoRenta;
                c.IP = cmp.IP;                        

            } else {

                var computadora = new Computadora();
                computadora.nombre = cdto.nombre;
                computadora.enLinea = true;
                // computadora.costoRenta = 10;
                computadora.IP = cdto.IP;

                db.Computadoras.Add(computadora);
                db.SaveChanges();

                c.nombre = computadora.nombre;
                c.idComputadora = computadora.idComputadora;
                c.enLinea = computadora.enLinea;
                c.costoRenta = computadora.costoRenta;
            }

            return c;            
        }

        [HttpPost]
        [Route("setDesktopOnline")]
        public List<ComputadoraDTO> SetDesktopStatus([FromBody] Computadora c)
        {
            var registros = db.Database.SqlQuery<ComputadoraDTO>("servidor.ActualizarEstadoComputadora @idComputadora, @enLinea",
                new SqlParameter("idComputadora", c.idComputadora),
                new SqlParameter("enLinea", c.enLinea)).FirstOrDefault();

            return db.Computadoras.Where(w => w.enLinea == true).Select(s =>
                    new ComputadoraDTO
                    {
                        idComputadora = s.idComputadora,
                        nombre = s.nombre,
                        enLinea = s.enLinea,
                        costoRenta = s.costoRenta,
                        IP = s.IP
                    }).ToList();
        }

        // GET: api/Computadora/5
        [ResponseType(typeof(Computadora))]
        public IHttpActionResult GetComputadora(int id)
        {
            Computadora computadora = db.Computadoras.Find(id);
            if (computadora == null)
            {
                return NotFound();
            }

            return Ok(computadora);
        }

        // PUT: api/Computadora/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComputadora(int id, Computadora computadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computadora.idComputadora)
            {
                return BadRequest();
            }

            db.Entry(computadora).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputadoraExists(id))
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

        // POST: api/Computadora
        [ResponseType(typeof(Computadora))]
        public IHttpActionResult PostComputadora(Computadora computadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Computadoras.Add(computadora);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = computadora.idComputadora }, computadora);
        }

        // DELETE: api/Computadora/5
        [ResponseType(typeof(Computadora))]
        public IHttpActionResult DeleteComputadora(int id)
        {
            Computadora computadora = db.Computadoras.Find(id);
            if (computadora == null)
            {
                return NotFound();
            }

            db.Computadoras.Remove(computadora);
            db.SaveChanges();

            return Ok(computadora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComputadoraExists(int id)
        {
            return db.Computadoras.Count(e => e.idComputadora == id) > 0;
        }
    }
}