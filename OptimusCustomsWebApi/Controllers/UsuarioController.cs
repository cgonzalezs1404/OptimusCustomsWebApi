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
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Usuario> Get([FromQuery] int? idTipoUsuario, [FromQuery] string? RFC)
        {
            try
            {
                return DataAccess.Instance.GetUsuarios(idTipoUsuario, RFC);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetUsuarios()", ex.Message);
            }
            return null;
        }

        [HttpGet]
        [Route("login")]
        public Usuario Get([FromQuery] string UserName, [FromQuery] string Paswword)
        {
            return DataAccess.Instance.GetUsuario(UserName, Paswword);
        }

        [HttpGet("{id:int}")]
        public Usuario Get(int id)
        {
            try
            {
                return DataAccess.Instance.GetUsuario(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetUsuario()", ex.Message);
            }
            return null;
        }

        [HttpPost]
        public void Create([FromBody] Usuario _model)
        {
            try
            {
                DataAccess.Instance.InsertUsuario(_model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en InsertCliente()", ex.Message);
            }
        }

        [HttpPut]
        public void Update([FromBody] Usuario _model)
        {
            try
            {
                DataAccess.Instance.UpdateCliente(_model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateCliente()", ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            try
            {
                DataAccess.Instance.DeleteCliente(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteCliente()", ex.Message);
            }
        }
    }
}
