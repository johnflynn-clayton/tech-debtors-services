using Cmh.Vmf.Infrastructure.AspNet.Controllers;
using CMH.MobileHomeTracker.Domain.Services;
using CMH.MobileHomeTracker.Infrastructure.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : BaseController<Guid>
    {
        private readonly IHomeService _service;
        private readonly HomeMapper _mapper;
        public readonly LocationRecordMapper _locationMapper;

        public HomeController(IHomeService service, HomeMapper mapper, LocationRecordMapper locationMapper)
        {
            _service = service;
            _mapper = mapper;
            _locationMapper = locationMapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Dto.Home>), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAsync()
        {
            var model = await _service.GetAllAsync();

            return Ok(model.Select(_mapper.Map).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dto.Home), 200)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 400)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 404)]
        public async Task<IActionResult> GetAsync(string id)
        {
            var parsedId = ParseGuid(id);
            var model = await _service.GetAsync(parsedId);

            return Ok(_mapper.Map(model));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Dto.Home), 201)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 400)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> InsertAsync(Dto.Home dto)
        {
            var model = _mapper.Map(dto);
            var createdModel = await _service.CreateAsync(model);

            return Created(_mapper.Map(createdModel));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 400)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Dto.Home dto)
        {
            var model = _mapper.Map(dto);

            await _service.UpdateAsync(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 400)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var parsedId = ParseGuid(id);

            await _service.DeleteAsync(parsedId);

            return NoContent();
        }

        [HttpGet("location/{id}")]
        [ProducesResponseType(typeof(Dto.LocationRecord), 200)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 400)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Cmh.Vmf.Infrastructure.AspNet.Dto.ErrorDetails), 404)]
        public async Task<IActionResult> GetHomeLocation(string id)
        {
            var parseId = ParseGuid(id);

            var model = await _service.GetCurrentLocationForHomeId(parseId);

            return Ok(_locationMapper.Map(model));
        }
    }
}
