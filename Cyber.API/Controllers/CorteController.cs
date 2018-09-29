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
using Cyber.API.Models.DTO;
using System.Data.Entity.Core.Objects;

namespace Cyber.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Corte")]
    public class CorteController : ApiController
    {
        private CyberModel db = new CyberModel();

        // GET: api/Corte
        public IQueryable<Corte> GetCortes()
        {
            return db.Cortes;
        }

        [HttpGet]
        [Route("cortes")]
        public List<CorteDTO> ObtenerCortes()
        {
            return db.Cortes.Where(w => w.fechaFin != null)
                .Select(s => new CorteDTO
                {
                    idCorte = s.idCorte,
                    idUsuario = s.idUsuario,
                    montoInicial = s.montoInicial,
                    montoVentas = s.montoVentas,
                    montoFinal = s.montoFinal,
                    diferencia = s.diferencia,
                    fechaInicio = s.fechaInicio,
                    fechaFin = s.fechaFin
                }).OrderByDescending(o => o.fechaInicio).ToList();
        }

        // GET: api/Corte/5
        [ResponseType(typeof(Corte))]
        public IHttpActionResult GetCorte(int id)
        {
            Corte corte = db.Cortes.Find(id);
            if (corte == null)
            {
                return NotFound();
            }

            return Ok(corte);
        }

        // PUT: api/Corte/5
        [HttpPut]
        public IHttpActionResult PutCorte([FromBody] CorteDTO c)
        {
            Corte corte = new Corte();
            corte.idCorte = c.idCorte;
            corte.idUsuario = c.idUsuario;
            corte.montoInicial = c.montoInicial;
            corte.montoFinal = c.montoFinal;
            corte.montoVentas = c.montoVentas;
            corte.diferencia = c.diferencia;
            corte.fechaInicio = c.fechaInicio;
            corte.fechaFin = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(corte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorteExists(corte.idCorte))
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

        [HttpPost]
        [Route("crear")]
        public CorteDTO PostCorte([FromBody]int idUsuario)
        {
            
            if (!ModelState.IsValid)
            {
                throw new Exception("El modelo es invalido");
            }

            if (idUsuario == 0)
            {
                throw new Exception("idUsuario no puede ser cero");
            }

            Corte corte = null;
            if (!CorteExistsByDate())
            {
                corte = new Corte
                {
                    idUsuario = idUsuario,
                    montoInicial = 150,
                    fechaInicio = DateTime.Now
                };

                db.Cortes.Add(corte);
                db.SaveChanges();
            } else
            {
                corte = db.Cortes.Where(w => DbFunctions.TruncateTime(w.fechaInicio) == DbFunctions.TruncateTime(DateTime.Now) && w.fechaFin == null).FirstOrDefault();
            }

            CorteDTO c = new CorteDTO();
            c.idCorte = corte.idCorte;
            c.idUsuario = corte.idUsuario;
            c.montoInicial = corte.montoInicial;
            c.fechaInicio = corte.fechaInicio;

            return c;
        }

        // DELETE: api/Corte/5
        [ResponseType(typeof(Corte))]
        public IHttpActionResult DeleteCorte(int id)
        {
            Corte corte = db.Cortes.Find(id);
            if (corte == null)
            {
                return NotFound();
            }

            db.Cortes.Remove(corte);
            db.SaveChanges();

            return Ok(corte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CorteExists(int id)
        {
            return db.Cortes.Count(e => e.idCorte == id) > 0;
        }

        private bool CorteExistsByDate()
        {
            return db.Cortes.Count(e => DbFunctions.TruncateTime(e.fechaInicio) == DbFunctions.TruncateTime(DateTime.Now) && e.fechaFin == null) > 0;
        }
    }
}