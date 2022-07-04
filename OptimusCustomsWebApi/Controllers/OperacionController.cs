using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public OperacionController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
    }
}
