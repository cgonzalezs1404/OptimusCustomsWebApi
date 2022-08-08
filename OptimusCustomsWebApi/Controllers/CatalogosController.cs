using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptimusCustomsWebApi.Data;
using OptimusCustomsWebApi.Enum;
using OptimusCustomsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogosController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public CatalogosController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public List<Catalogo> Privilegios(int id)
        {
            try
            {
                if (id == 1) { return DataAccess.Instance.GetCatalogo(TipoCatalogo.Privilegio); }
                if (id == 2) { return DataAccess.Instance.GetCatalogo(TipoCatalogo.EstadoFactura); }
                if (id == 3) { return DataAccess.Instance.GetCatalogo(TipoCatalogo.TipoFactura); }
                if (id == 4) { return DataAccess.Instance.GetCatalogo(TipoCatalogo.TipoOperacion); }
                if (id == 5) { return DataAccess.Instance.GetCatalogo(TipoCatalogo.Usuarios); }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetUsuarios()", ex.Message);
            }
            return null;
        }
    }
}
