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

            var family = await _apiHelper.CreateFamily(id, createFamilyDTO.FamilyName);

            if(family == null) return BadRequest(new {
                errors = "As normal user you can't create family."
            });

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

            var family = await _apiHelper.JoinToFamily(joinToFamilyDTO.InvitationCode, id);

            if(family == null) return BadRequest(new {
                errors = "Bad invitation code, you are not normal user or you are already in family."
            });

            var familyToReturn = await _apiHelper.ReturnFamilyInfo(family.Id, id);

            return familyToReturn;
        }

        ///<summary>
        ///Send mail with invitation code to family by PSY user
        ///</summary>
        [HttpPost("sendMailWithCode")]
        public async Task<IActionResult> SendMailWithCode(SendMailDTO sendMailDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var isMailSend = await _apiHelper.SendMailWithCode(sendMailDTO.Email, sendMailDTO.FamilyId, id);
            
            if(!isMailSend) return BadRequest(new {
                errors = "Invalid familyId or you are not owner of this family."
            });

            return Ok(new {
                message = "Mail has been sent."
            });
        }
    }
}