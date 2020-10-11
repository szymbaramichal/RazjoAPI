using System.Threading.Tasks;
using AutoMapper;
using API.DTOs;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using API.Models;

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
        public async Task<IActionResult> RegisterSingleUser(RegisterSingleUserDTO registerUserDTO)
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

            return new ReturnUserDTO {
                Token = _tokenHelper.CreateToken(user)
            };
        } 
    }
}