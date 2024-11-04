namespace GenericCrud.Controllers.Base
{
    using GenericCrud.Dto.Base.Interfaces;
    using GenericCrud.Filter.Interfaces;
    using GenericCrud.Filter.Sort;
    using GenericCrud.Services.Interfaces.Base;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class BaseController<TDto, TFilter> : ControllerBase
        where TDto : class, IBaseDTO, new()
        where TFilter : class, IDtoFilter, new()
    {
        private readonly IService<TDto, TFilter> _service;

        public BaseController(IService<TDto, TFilter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("Find")]
        public virtual ActionResult<IEnumerable<TDto>> Find([FromQuery] TFilter filter, CountType? count)
        {
            var result = _service.Find(filter, count.GetValueOrDefault());
            if (count.GetValueOrDefault() != CountType.No)
            {
                Response.Headers.Add("X-Total-Count", result.Count.ToString());
            }
            return result.Data.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = (dto as dynamic).Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
