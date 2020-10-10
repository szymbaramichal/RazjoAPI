using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Helpers;
using DTOs;
using AutoMapper;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        public ValuesController(IApiHelper apiHelper, IMapper mapper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Value>> GetValueById(string id)
        {
            return await _apiHelper.GetValueById(id);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Value>> AddValue(AddValuesDTO addValuesDTO)
        {
            var mappedValue = _mapper.Map<Value>(addValuesDTO);
            var value = await _apiHelper.AddValue(mappedValue);
            return value;
        }

    }
}