using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class CorteDTO
    {
        public int idCorte { get; set; }
        public int idUsuario { get; set; }
        public decimal? montoInicial { get; set; }
        public decimal? montoFinal { get; set; }
        public decimal? montoVentas { get; set; }
        public decimal? diferencia { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public UsuarioDTO Usuario { get; set; }

        public string montoInicialStr
        {
            get
            {
                return string.Format("{0:C}", this.montoInicial);
            }
        }

        public string montoFinalStr
        {
            get
            {
                return string.Format("{0:C}", this.montoFinal);
            }
        }

        public string montoVentasStr
        {
            get
            {
                return string.Format("{0:C}", this.montoVentas);
            }
        }

        public string diferenciaStr
        {
            get
            {
                return string.Format("{0:C}", this.diferencia);
            }
        }

        public string fechaInicioStr
        {
            get
            {
                return this.fechaInicio.ToString("dd/MM/yyyy h:mm tt");
            }
        }

        public string fechaFinStr
        {
            get
            {
                if (this.fechaFin != null)
                {
                    return this.fechaFin.Value.ToString("dd/MM/yyyy h:mm tt");
                }

                return string.Empty;
            }
        }
    }
}