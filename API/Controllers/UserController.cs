using System.Threading.Tasks;
using AutoMapper;
using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        /// Register user on platform, PSY-psychlogist/USR-user
        ///</summary>
        /// <param name="registerUserDTO">Email, Password and Role</param>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterSingleUser(RegisterUserDTO registerUserDTO)
        {
            var mappedUser = _mapper.Map<User>(registerUserDTO);

            var isAdded = await _apiHelper.AddUser(mappedUser, registerUserDTO.Password);

            if(isAdded) return Ok(new { 
                message = "Pomyślnie zarejestrowano."
            });
            else return BadRequest(new {
                errors = "Użytkownik o podanym adresie email już jest zarejestrowany."
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
                    errors = "Niepoprawne hasło lub email."
                });
            }

            var userToReturn = new ReturnUserDTO();
            userToReturn.Families = new List<ReturnFamilyDTO>();
            
            for (int i = 0; i < user.FamilyId.Count; i++)
            {
                userToReturn.Families.Add(await _apiHelper.ReturnFamilyInfo(user.FamilyId[i], user.Id));
            }

            var privateNotes = await _apiHelper.ReturnUserPrivateNotes(user.Id);
            userToReturn.PrivateNotes = new List<ReturnPrivateNoteDTO>();
            for (int i = 0; i < privateNotes.Count; i++)
            {
                userToReturn.PrivateNotes.Add(_mapper.Map<ReturnPrivateNoteDTO>(privateNotes[i]));
            }

            userToReturn.Token = _tokenHelper.CreateToken(user);
            userToReturn.UserInfo = _mapper.Map<UserInfoDTO>(user);

            return userToReturn;
        }

        ///<summary>
        /// Update user Firstname and Surname.
        ///</summary>
        /// <param name="updateUserInfoDTO">Input object</param>
        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<UserInfoDTO>> UpdateUserInfo(UpdateUserInfoDTO updateUserInfoDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var user = await _apiHelper.UpdateUserInfo(id, updateUserInfoDTO.FirstName, updateUserInfoDTO.Surname);

            var mappedUser = _mapper.Map<UserInfoDTO>(user);
            
            return mappedUser;
        }
    }
}