using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    public class Operacion
    {
        public int IdOperacion { get; set; }
        public int IdTipoOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public int? IdUsuario { get; set; }
        public string RazonSocial { get; set; }
        public int? IdFactura { get; set; }
        public string FolioFactura { get; set; }
        public int? IdComprobantePago { get; set; }
        public int? IdComplementoPago { get; set; }
        public int? IdPruebaEntrega { get; set; }
        public string NumOperacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
