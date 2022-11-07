using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptimusCustomsWebApi.Data;
using OptimusCustomsWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public OperacionController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOperacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validate")]
        public Operacion Validate([FromQuery] string numOperacion)
        {
            return DataAccess.Instance.ValidateOperacion(numOperacion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ObjectResult Update([FromBody] Operacion model)
        {
            if (model != null)
            {
                if (DataAccess.Instance.UpdOperacion(model))
                    return Ok(model);
                else
                    return BadRequest(null);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("document")]
        public ObjectResult Update([FromBody] Documento model)
        {
            if (model != null)
            {
                if (DataAccess.Instance.UpdDocumento(model))
                    return Ok(model);
                else
                    return BadRequest(null);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("document")]
        public ObjectResult Create([FromBody] Documento model)
        {
            if (model != null)
            {
                if (DataAccess.Instance.InsDocumento(model))
                    return Ok(model);
                else
                    return BadRequest(null);
            }
            return null;
        }

        [HttpGet]
        [Route("document")]
        public Stream GetDocumento([FromQuery] int idOperacion, [FromQuery] int idTipoDocumento)
        {
            Stream result = null;
            try
            {
                result = DataAccess.Instance.GetDocumento(idOperacion, idTipoDocumento);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
