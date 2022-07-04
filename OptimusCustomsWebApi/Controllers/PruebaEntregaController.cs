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
    public class PruebaEntregaController : ControllerBase
    {
        private readonly ILogger<PruebaEntregaController> _logger;

        public PruebaEntregaController(ILogger<PruebaEntregaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{idPruebaEntrega:int}")]
        public PruebaEntrega Get(int idPruebaEntrega)
        {
            return DataAccess.Instance.GetPruebaEntrega(idPruebaEntrega);
        }
        [HttpPost]
        public void Create([FromBody] PruebaEntrega model)
        {
            try
            {
                DataAccess.Instance.InsertPruebaEntrega(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void Update([FromBody] PruebaEntrega model)
        {
            try
            {
                DataAccess.Instance.UpdatePruebaEntrega(model);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{idPruebaEntrega:int}")]
        public void Delete(int idPruebaEntrega)
        {
            try
            {
                DataAccess.Instance.DeletePruebaEntrega(idPruebaEntrega);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
