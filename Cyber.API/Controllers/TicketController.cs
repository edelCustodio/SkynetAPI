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
using Cyber.API.Helper;
using Cyber.API.Models.DTO;

namespace Cyber.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Ticket")]
    public class TicketController : ApiController
    {
        private CyberModel db = new CyberModel();

        // GET: api/Ticket
        public IQueryable<Ticket> GetTickets()
        {
            return db.Tickets;
        }

        [HttpGet]
        [Route("getTicketsPending")]
        public List<TicketDTO> GetTicketsPending()
        {
            var tickets =  db.Tickets.Where(w => w.status == false && w.eliminado == false)
                    .Select(s => new TicketDTO
                    {
                        idTicket = s.idTicket,
                        cambio = s.cambio,
                        fecha = s.fecha,
                        eliminado = s.eliminado,
                        idRegistro = s.idRegistro,
                        idUsuario = s.idUsuario,
                        pago = s.pago,
                        status = s.status,
                        Detalle = s.Detalle.Where(w1 => w1.eliminado == false).Select(sd => new TicketDetalleDTO
                        {
                            idTicketDetalle = sd.idTicketDetalle,
                            idTicket = sd.idTicket,
                            cantidad = sd.cantidad,
                            precio = sd.precio,
                            idProducto = sd.idProducto,
                            eliminado = sd.eliminado
                        }).ToList()
                    }).ToList();

            return tickets;
        }

        [HttpPost]
        [Route("createTicket")]
        public int CreateTicket([FromBody] Ticket ticket)
        {
            var registros = db.Database.SqlQuery<int>("servidor.CrearTicket @total, @pago, @cambio, @idUsuario",
                new SqlParameter("total", ticket.total),
                new SqlParameter("pago", ticket.pago),
                new SqlParameter("cambio", ticket.cambio),
                new SqlParameter("idUsuario", ticket.idUsuario)).FirstOrDefault();

            return registros;
        }

        [HttpPost]
        [Route("createTicketDetalle")]
        public int CreateTicketDetalle([FromBody] TicketDetalleInsert tDetalle)
        {
            var crearTicketDetalle = db.Database.ExecuteSqlCommand(tDetalle.strInsert);

            return crearTicketDetalle;
        }

        [HttpPost]
        [Route("updateTicketDetalle")]
        public int UpdateTicketDetalle([FromBody] TicketDetalleUpdate update)
        {
            var updateTicketDetalle = db.Database.ExecuteSqlCommand(@"UPDATE [Entidad].[TicketDetalle] 
                    SET cantidad = @cantidad
                       ,precio = @precio 
                  WHERE idTicketDetalle = @idTicketDetalle",
                new SqlParameter("cantidad", update.cantidad),
                new SqlParameter("precio", update.precio),
                new SqlParameter("idTicketDetalle", update.idTicketDetalle));

            return updateTicketDetalle;
        }

        [HttpPost]
        [Route("deleteTicketDetalle")]
        public int DeleteTicketDetalle([FromBody] TicketDetalleUpdate delete)
        {
            var deleteTicketDetalle = db.Database.ExecuteSqlCommand("servidor.EliminarTicketDetalle @idTicketDetalle",
                new SqlParameter("idTicketDetalle", delete.idTicketDetalle));

            return deleteTicketDetalle;
        }

        [HttpPost]
        [Route("deleteTicket")]
        public int DeleteTicket([FromBody] TicketDTO ticket)
        {
            var deleteTicket = db.Database.ExecuteSqlCommand("UPDATE [Entidad].[Ticket] SET eliminado = 1 WHERE idTicket = @idTicket",
                new SqlParameter("idTicket", ticket.idTicket));

            return deleteTicket;
        }

        [HttpPost]
        [Route("payTicket")]
        public int PayTicket([FromBody] TicketDTO ticket)
        {
            var deleteTicket = db.Database.ExecuteSqlCommand(@"UPDATE [Entidad].[Ticket] 
                        SET status = 1,
                            pago = @pago,
                            cambio = @cambio,
                            total = @total
                    WHERE idTicket = @idTicket",
                new SqlParameter("idTicket", ticket.idTicket),
                new SqlParameter("pago", ticket.pago),
                new SqlParameter("cambio", ticket.cambio),
                new SqlParameter("total", ticket.total));

            return deleteTicket;
        }

        // GET: api/Ticket/5
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> GetTicket(int id)
        {
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // PUT: api/Ticket/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTicket(int id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.idTicket)
            {
                return BadRequest();
            }

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Ticket
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> PostTicket(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ticket.idTicket }, ticket);
        }

        // DELETE: api/Ticket/5
        //[ResponseType(typeof(Ticket))]
        //public async Task<IHttpActionResult> DeleteTicket(int id)
        //{
        //    Ticket ticket = await db.Tickets.FindAsync(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Tickets.Remove(ticket);
        //    await db.SaveChangesAsync();

        //    return Ok(ticket);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return db.Tickets.Count(e => e.idTicket == id) > 0;
        }
    }
}