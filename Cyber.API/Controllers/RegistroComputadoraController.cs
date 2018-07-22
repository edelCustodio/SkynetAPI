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
using System.Data.SqlClient;
using Cyber.API.Models.DTO;
using Cyber.API.Helper;

namespace Cyber.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Record")]
    public class RegistroComputadoraController : ApiController
    {
        private CyberModel db = new CyberModel();

        // GET: api/RegistroComputadora
        public IQueryable<RegistroComputadora> GetRegistroComputadoras()
        {
            return db.RegistroComputadoras;
        }

        [HttpPost]
        [Route("desktopRecord")]
        public RegistroComputadoraDTO GetRegistrosFaltantes([FromBody] RecordParameters parameters)
        {
            var registro = db.Database.SqlQuery<RegistroComputadoraDTO>("cliente.GuardarRegistroComputadora @idComputadora, @fecha, @minutos, @idUsuario",
                new SqlParameter("idComputadora", parameters.idComputadora),
                new SqlParameter("fecha", parameters.fecha),
                new SqlParameter("minutos", parameters.minutos),
                new SqlParameter("idUsuario", parameters.idUsuario)).FirstOrDefault();

            return registro;
        }

        [HttpGet]
        [Route("getDesktopsInUse")]
        public IEnumerable<RegistroComputadoraDTO> GetComputadorasEnUso()
        {
            return db.RegistroComputadoras.Where(w => w.fechaFin == null && w.fechaInicio == DateTime.Now)
                    .Select(s => new RegistroComputadoraDTO
                    {
                        idRegistro = s.idRegistro,
                        idComputadora = s.idComputadora,
                        fechaInicio = s.fechaInicio,
                        fechaFin = s.fechaFin,
                        minutos = s.minutos,
                        pagado = s.pagado,
                        total = s.total,
                        totalPagar = s.totalPagar
                    }).ToList();
        }

        [HttpGet]
        [Route("getRecordsNoPay")]
        public IEnumerable<RegistroComputadoraDTO> GetRegistrosNoPagados()
        {
            return db.RegistroComputadoras.Where(w => w.pagado == false)
                    .Select(s => new RegistroComputadoraDTO
                    {
                        idRegistro = s.idRegistro,
                        idComputadora = s.idComputadora,
                        fechaInicio = s.fechaInicio,
                        fechaFin = s.fechaFin,
                        minutos = s.minutos,
                        pagado = s.pagado,
                        total = s.total,
                        totalPagar = s.totalPagar
                    }).ToList();
        }

        // GET: api/RegistroComputadora/5
        [ResponseType(typeof(RegistroComputadora))]
        public async Task<IHttpActionResult> GetRegistroComputadora(int id)
        {
            RegistroComputadora registroComputadora = await db.RegistroComputadoras.FindAsync(id);
            if (registroComputadora == null)
            {
                return NotFound();
            }

            return Ok(registroComputadora);
        }

        // PUT: api/RegistroComputadora/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRegistroComputadora(int id, RegistroComputadora registroComputadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroComputadora.idRegistro)
            {
                return BadRequest();
            }

            db.Entry(registroComputadora).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroComputadoraExists(id))
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

        // POST: api/RegistroComputadora
        [ResponseType(typeof(RegistroComputadora))]
        public async Task<IHttpActionResult> PostRegistroComputadora(RegistroComputadora registroComputadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistroComputadoras.Add(registroComputadora);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = registroComputadora.idRegistro }, registroComputadora);
        }

        // DELETE: api/RegistroComputadora/5
        [ResponseType(typeof(RegistroComputadora))]
        public async Task<IHttpActionResult> DeleteRegistroComputadora(int id)
        {
            RegistroComputadora registroComputadora = await db.RegistroComputadoras.FindAsync(id);
            if (registroComputadora == null)
            {
                return NotFound();
            }

            db.RegistroComputadoras.Remove(registroComputadora);
            await db.SaveChangesAsync();

            return Ok(registroComputadora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistroComputadoraExists(int id)
        {
            return db.RegistroComputadoras.Count(e => e.idRegistro == id) > 0;
        }
    }
}