using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using OptimusCustomsWebApi.Data;
using OptimusCustomsWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly ILogger<FacturaController> _logger;

        public FacturaController(ILogger<FacturaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ObjectResult Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] int idTipoFactura, [FromQuery] int idEstadoFactura, [FromQuery] int idUsuario)
        {
            var result = DataAccess.Instance.GetFacturas(fromDate, toDate, idTipoFactura, idEstadoFactura, idUsuario);
            if (result != null && result.Count != 0)
                return Ok(result);
            else if (result != null && result.Count == 0)
                return NotFound(null);
            else if (result == null)
                return BadRequest(null);
            else
                return null;
        }

        [HttpGet("{idFactura:int}")]
        public ObjectResult Get(int Id)
        {
            var result = DataAccess.Instance.GetFactura(Id);
            if (result != null)
                return Ok(result);
            else if (Id != 0 && result == null)
                return NotFound(null);
            else if (result == null)
                return BadRequest(null);
            else
                return null;
        }

        [HttpPost]
        public ObjectResult Create([FromBody] Factura model)
        {
            try
            {
                if (model != null)
                {
                    var result = DataAccess.Instance.InsertFactura(model);
                    if (result != null)
                        return Ok(result);
                    else
                        return BadRequest(null);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpPut]
        public void Update([FromBody] Factura model)
        {
            try
            {
                DataAccess.Instance.UpdateFactura(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{idFactura:int}")]
        public void Delete(int idFactura)
        {
            try
            {
                DataAccess.Instance.DeleteFactura(idFactura);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet]
        [Route("pdf")]
        public Stream GetDocumento([FromQuery] int idFactura)
        {
            Stream result = null;
            try
            {
                result = DataAccess.Instance.GetFacturaPDF(idFactura);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
