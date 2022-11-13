using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    public class PruebaEntrega
    {
        public int IdPruebaEntrega { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Url { get; set; }
    }
}
