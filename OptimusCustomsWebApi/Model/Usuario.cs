using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string DireccionFiscal { get; set; }
        public string Password { get; set; }
        public string TipoUsuario { get; set; }

    }
}
