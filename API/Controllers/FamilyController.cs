using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FamilyController : ControllerBase
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public FamilyController(IApiHelper apiHelper, ITokenHelper tokenHelper, IMapper mapper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ReturnFamilyDTO>> CreateFamily(CreateFamilyDTO createFamilyDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            if(await _apiHelper.ReturnUserRole(id) != "PSY") return BadRequest(new {
                errors = "As normal user you can't create family."
            });

            var family = await _apiHelper.CreateFamily(id, createFamilyDTO.FamilyName);

            var mappedFamily = _mapper.Map<ReturnFamilyDTO>(family);

            return mappedFamily;
        }
    }
}