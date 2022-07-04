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
    public class ComprobantePagoController : ControllerBase
    {
        private readonly ILogger<ComprobantePagoController> _logger;

        public ComprobantePagoController(ILogger<ComprobantePagoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{idComprobantePago:int}")]
        public ComprobantePago Get(int idComprobantePago)
        {
            return DataAccess.Instance.GetComprobantePago(idComprobantePago);
        }
        [HttpPost]
        public void Create([FromBody] ComprobantePago model)
        {
            try
            {
                DataAccess.Instance.InsertComprobantePago(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void Update([FromBody] ComprobantePago model)
        {
            try
            {
                DataAccess.Instance.UpdateComprobantePago(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{idComprobantePago:int}")]
        public void Delete(int idComprobantePago)
        {
            try
            {
                DataAccess.Instance.DeleteComprobantePago(idComprobantePago);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
