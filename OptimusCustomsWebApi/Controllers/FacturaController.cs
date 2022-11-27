using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public List<Factura> Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return DataAccess.Instance.GetFacturas(fromDate, toDate);
        }

        [HttpGet("{idFactura:int}")]
        public Factura Get(int idFactura)
        {
            return DataAccess.Instance.GetFactura(idFactura);
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
