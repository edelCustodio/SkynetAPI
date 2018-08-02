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

        [HttpGet]
        [Route("getDesktopByName")]
        public ComputadoraDTO GetDesktopByName([FromUri] string name)
        {
            var c = new ComputadoraDTO();

            var exists = db.Computadoras.Count(w => w.nombre == name) > 0;

            if (exists)
            {
                c = db.Computadoras.Where(w => w.nombre == name).Select(s =>
                        new ComputadoraDTO
                        {
                            idComputadora = s.idComputadora,
                            nombre = s.nombre,
                            enLinea = s.enLinea,
                            costoRenta = s.costoRenta,
                            IP = s.IP
                        }).FirstOrDefault();
            } else {

                var computadora = new Computadora();
                computadora.nombre = name;
                computadora.enLinea = true;
                // computadora.costoRenta = 10;
                computadora.IP = "127.0.0.1";

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