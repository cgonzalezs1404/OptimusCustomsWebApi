using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Proveedor
    {
        /// <summary>
        /// 
        /// </summary>
        public int IdProveedor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RazonSocial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RFC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DireccionFiscal { get; set; }
    }
}
