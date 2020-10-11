using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly ITokenHelper _tokenHelper;
        public CalendarController(IApiHelper apiHelper, ITokenHelper tokenHelper, IMapper mapper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _tokenHelper = tokenHelper;
        }
        
        ///<summary>
        ///Add note to calendar
        ///</summary>
        /// <param name="addCalendarNoteDTO">Input object</param>
        [HttpPost("addNote")]
        public async Task<ActionResult<ReturnCalendarNoteDTO>> AddCalendarNote(AddCalendarNoteDTO addCalendarNoteDTO) 
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);
            
            var mappedNote = _mapper.Map<CalendarNote>(addCalendarNoteDTO);
            
            var calendarNote = await _apiHelper.AddCalendarNote(mappedNote, id);

            return calendarNote;
        }

        ///<summary>
        ///Get notes from actual month by token
        ///</summary>
        [HttpGet("getLastNotes")]
        public async Task<ActionResult<List<ReturnCalendarNoteDTO>>> GetLastCalendarNotes()
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var notes = await _apiHelper.ReturnLastMonthNotes(id);

            return notes;
        }
    }
}