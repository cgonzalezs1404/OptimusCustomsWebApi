using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Model
{
    public class Documento
    {
        public int IdOperacionDetalle { get; set; }
        public int IdOperacion { get; set; }
        public int IdTipoDocumento { get; set; }
        public byte[] SourceFile { get; set; }
    }
}
