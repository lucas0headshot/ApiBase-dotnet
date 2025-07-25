using CoreBackend.src.Application;
using CoreBackend.src.DTOs;
using CoreBackend.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseControllerGuid<T> : ControllerBase where T : EntityGuid
    {
        private readonly IApplicationBase<T> _applicationBase;
        protected BaseControllerGuid(IApplicationBase<T> applicationBase)
        {
            _applicationBase = applicationBase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromForm] QueryParams queryParams)
        {
            try
            {
                var result = await _applicationBase.GetAllAsync(queryParams);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var result = await _applicationBase.GetAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
