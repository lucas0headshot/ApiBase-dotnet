using Application.ApplicationBase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _applicationBase.GetAllAsync();

                return Ok(result);
            }
            catch(Exception ex)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T entity)
        {
            try
            {
                var result = await _applicationBase.CreateAsync(entity);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] T entity)
        {
            try
            {
                await _applicationBase.UpdateAsync(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _applicationBase.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
