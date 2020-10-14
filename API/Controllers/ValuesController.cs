using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Helpers;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
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

        ///<summary>
        /// Test method to test authorization
        ///</summary>
        /// <param name="id">Id of value</param>
        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Value>> GetValueById(string id)
        {
            return await _apiHelper.GetValueById(id);
        }

        ///<summary>
        /// Test method to add value 
        ///</summary>
        /// <param name="addValuesDTO">Input object</param>
        [HttpPost("add")]
        public async Task<ActionResult<Value>> AddValue(AddValuesDTO addValuesDTO)
        {
            var mappedValue = _mapper.Map<Value>(addValuesDTO);
            var value = await _apiHelper.AddValue(mappedValue);
            return value;
        }

        [HttpGet]
        public IActionResult TestMethod()
        {
            return Ok("Dziala");
        }

    }
}