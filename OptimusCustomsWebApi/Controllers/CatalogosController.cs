using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptimusCustomsWebApi.Data;
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
        public List<Privilegio> Privilegios(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        return DataAccess.Instance.GetPrivilegios();
                    default:
                        return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetUsuarios()", ex.Message);
            }
            return null;
        }
    }
}
