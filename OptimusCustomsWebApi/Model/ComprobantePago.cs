using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    public class ComprobantePago
    {
        public int IdComprobantePago { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Url { get; set; }
    }
}
