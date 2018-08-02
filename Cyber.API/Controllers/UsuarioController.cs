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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cyber.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UsuarioController : ApiController
    {
        private CyberModel db = new CyberModel();

        // GET: api/Usuario
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios;
        }

        
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate([FromBody] Usuario usuario)
        {
            var loginResponse = new LoginResponse { };

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;
            var findUser = new Usuario();
            if (usuario != null)
            {
                findUser = db.Usuarios.Where(w => w.usuario1 == usuario.usuario1).FirstOrDefault();
                var hashingPassword = Hashing.HashPassword(usuario.contraseña);
                isUsernamePasswordValid = Hashing.ValidatePassword(usuario.contraseña, findUser.contraseña);
            }
                
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = createToken(usuario.usuario1, findUser.idUsuario);
                //return the token
                return Ok<string>(token);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
        }

        // POST: api/Usuario
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = db.Usuarios.Count(w => w.correoElectronico == usuario.correoElectronico && w.usuario1 == usuario.usuario1) > 0;

            if (!exists)
            {
                var hashingPassword = Hashing.HashPassword(usuario.contraseña);
                usuario.contraseña = hashingPassword;

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return Json(new { id = usuario.idUsuario, status = HttpStatusCode.OK });
            }
            else
            {
                return Json(new { id = 0, status = HttpStatusCode.Conflict }); // return 409
            }
        }

        private string createToken(string username, int idUsuario)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

       

        // DELETE: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.idUsuario == id) > 0;
        }
    }
}