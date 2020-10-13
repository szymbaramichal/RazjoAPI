using System.Threading.Tasks;
using AutoMapper;
using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly ITokenHelper _tokenHelper;
        public UserController(IApiHelper apiHelper, IMapper mapper, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _apiHelper = apiHelper;
        }

        ///<summary>
        /// Register user on platform, PSY-psychlogist, USR-user, PAR-parent
        ///</summary>
        /// <param name="registerUserDTO">Email, Password and Role</param>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterSingleUser(RegisterUserDTO registerUserDTO)
        {
            var mappedUser = _mapper.Map<User>(registerUserDTO);

            var isAdded = await _apiHelper.AddUser(mappedUser, registerUserDTO.Password);

            if(isAdded) return Ok(new { 
                message = "Registered successfully."
            });
            else return BadRequest(new {
                errors = "User with passed in email already exists."
            });
        }

        ///<summary>
        /// Login user in application
        ///</summary>
        /// <param name="loginUserDTO">Input object</param>
        [HttpPost("login")]
        public async Task<ActionResult<ReturnUserDTO>> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _apiHelper.Login(loginUserDTO.Email, loginUserDTO.Password);

            if(user == null) 
            {
                return BadRequest(new {
                    errors = "Invalid password or email."
                });
            }
            
            var notes = await _apiHelper.ReturnActualMonthNotes(user.Id);

            var userToReturn = new ReturnUserDTO();

            if(user.Role == "USR")
            {
                userToReturn.Token = _tokenHelper.CreateToken(user);

                List<ReturnCalendarNoteDTO> mappedNotes = new List<ReturnCalendarNoteDTO>();

                foreach (var note in notes)
                {
                    mappedNotes.Add(_mapper.Map<ReturnCalendarNoteDTO>(note));
                }
                userToReturn.CalendarNotes = mappedNotes;
            }
            else 
            {
                userToReturn.Token = _tokenHelper.CreateToken(user);
                userToReturn.Families = new List<ReturnFamilyDTO>();
                for (int i = 0; i < user.FamilyId.Count; i++)
                {
                    userToReturn.Families.Add(await _apiHelper.ReturnFamilyInfo(user.FamilyId[i]));
                }
            }

            userToReturn.UserInfo = _mapper.Map<UserInfoDTO>(user);

            return userToReturn;
        }
    }
}