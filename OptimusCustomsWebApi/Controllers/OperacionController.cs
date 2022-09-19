using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptimusCustomsWebApi.Data;
using OptimusCustomsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionController : ControllerBase
    {
        public OperacionController()
        {

        }

        [HttpGet]
        public ObjectResult Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = DataAccess.Instance.GetOperaciones(fromDate, toDate);
            if (result != null && result.Count != 0)
                return Ok(result);
            else if (result != null && result.Count == 0)
                return NotFound(null);
            else if (result == null)
                return BadRequest(null);
            else
                return null;
        }

        [HttpGet("{Id:int}")]
        public ObjectResult Get(int Id)
        {
            var result = DataAccess.Instance.GetOperacion(Id);
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
        public ObjectResult Create([FromBody] Operacion model)
        {
            if (model != null)
            {
                if (DataAccess.Instance.InsOperacion(model))
                    return Ok(model);
                else
                    return BadRequest(null);
            }
            return null;

        }

        [HttpGet]
        [Route("validate")]
        public Operacion Validate([FromQuery] string numOperacion)
        {
            return DataAccess.Instance.ValidateOperacion(numOperacion);
        }

        [HttpPut]
        public ObjectResult Update([FromBody] Operacion model)
        {
            if(model != null)
            {
                if (DataAccess.Instance.UpdOperacion(model))
                    return Ok(model);
                else
                    return BadRequest(null);
            }
            return null;
        }
    }
}
