using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string correoElectronico { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public int idTipoUsuario { get; set; }
        public TipoUsuarioDTO TipoUsuario { get; set; }

    }
}