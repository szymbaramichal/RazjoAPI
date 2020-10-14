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

        ///<summary>
        /// Create family
        ///</summary>
        /// <param name="createFamilyDTO">Input object</param>
        [HttpPost("create")]
        public async Task<ActionResult<ReturnFamilyDTO>> CreateFamily(CreateFamilyDTO createFamilyDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            if(await _apiHelper.ReturnUserRole(id) != "PSY") return BadRequest(new {
                errors = "As normal user you can't create family."
            });

            var family = await _apiHelper.CreateFamily(id, createFamilyDTO.FamilyName);

            var familyToReturn = await _apiHelper.ReturnFamilyInfo(family.Id, id);

            return familyToReturn;
        }

        ///<summary>
        ///Join to existing family
        ///</summary>
        /// <param name="joinToFamilyDTO">Input object</param>
        [HttpPost("join")]
        public async Task<ActionResult<ReturnFamilyDTO>> JoinToFamily(JoinToFamilyDTO joinToFamilyDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);
            
            if(await _apiHelper.ReturnUserRole(id) != "USR") return BadRequest(new {
                errors = "Only as normal user you can join to family."
            });

            var family = await _apiHelper.JoinToFamily(joinToFamilyDTO.InvitationCode, id);

            if(family == null) return BadRequest(new {
                errors = "Bad invitation code or you are already in family."
            });

            var familyToReturn = await _apiHelper.ReturnFamilyInfo(family.Id, id);

            return familyToReturn;
        }
    }
}