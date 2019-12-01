using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plumsail.Api.Models.Options;
using Plumsail.Api.Models.Somethings;
using Plumsail.Api.Utils;
using Plumsail.Domain;
using Plumsail.Repository.Interfaces;

namespace Plumsail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomethingsController : ControllerBase
    {
        private readonly ISomethingRepository _somethingRepository;
        private readonly ISomethingTypeRepository _somethingTypeRepository;
        private readonly IMapper _mapper;

        public SomethingsController(ISomethingRepository somethingRepository, ISomethingTypeRepository somethingTypeRepository, IMapper mapper)
        {
            _somethingRepository = somethingRepository;
            _somethingTypeRepository = somethingTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SomethingViewModel>> Get()
        {
            var somethings = await _somethingRepository.GetAll();

            return _mapper.Map<IEnumerable<SomethingViewModel>>(somethings);
        }

        [HttpGet]
        [Route("options")]
        public async Task<OptionsViewModel> GetOptions()
        {
            var typeOptions = await _somethingTypeRepository.GetAll();

            return new OptionsViewModel
            {
                TypeOptions = _mapper.Map<List<IntOption>>(typeOptions),
                StatusOptions = EnumService.GetIntOptions<SomethingStatus>()
            };
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SomethingCreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int id = await _somethingRepository.Add(_mapper.Map<Something>(model));
            var something = await _somethingRepository.Get(id);

            return Ok(_mapper.Map<SomethingViewModel>(something));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] SomethingEditViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _somethingRepository.Contains(model.Id)) return NotFound(new { error = $"Объект c id = {model.Id} не найден в базе!" });

            await _somethingRepository.Edit(_mapper.Map<Something>(model));
            var something = await _somethingRepository.Get(model.Id);

            return Ok(_mapper.Map<SomethingViewModel>(something));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            if (!await _somethingRepository.Contains(id)) return NotFound(new { error = $"Объект c id = {id} не найден в базе!" });

            await _somethingRepository.Delete(id);

            return Ok();
        }
    }
}