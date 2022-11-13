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
    public class ComplementoPagoController : ControllerBase
    {
        private readonly ILogger<ComplementoPagoController> _logger;

        public ComplementoPagoController(ILogger<ComplementoPagoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{idComplementoPago:int}")]
        public ComplementoPago Get(int idComplementoPago)
        {
            return DataAccess.Instance.GetComplementoPago(idComplementoPago);
        }
        [HttpPost]
        public void Create([FromBody] ComplementoPago model)
        {
            try
            {
                DataAccess.Instance.InsertComplementoPago(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void Update([FromBody] ComplementoPago model)
        {
            try
            {
                DataAccess.Instance.UpdateComplementoPago(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{idComplementoPago:int}")]
        public void Delete(int idComplementoPago)
        {
            try
            {
                DataAccess.Instance.DeleteComplementoPago(idComplementoPago);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
