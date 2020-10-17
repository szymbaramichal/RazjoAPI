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

            var family = await _apiHelper.ReturnFamilyInfo(addCalendarNoteDTO.FamilyId, id);

            if(family == null) return BadRequest(new {
                errors = "Invalid family id or you do not belong to this family."
            });

            var mappedNote = _mapper.Map<CalendarNote>(addCalendarNoteDTO);
            
            var calendarNote = await _apiHelper.AddCalendarNote(mappedNote, id);

            return _mapper.Map<ReturnCalendarNoteDTO>(calendarNote);
        }

        ///<summary>
        ///Get notes from actual month by token
        ///</summary>
        [HttpGet("getLastNotes")]
        public async Task<ActionResult<List<ReturnCalendarNoteDTO>>> GetLastCalendarNotes(GetNotesForActualMonthDTO getNotesForActualMonthDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var notes = await _apiHelper.ReturnActualMonthNotes(getNotesForActualMonthDTO.FamilyId, id);

            if(notes == null) return BadRequest(new {
                errors = "Invalid family id or you do not belong to this family."
            });

            List<ReturnCalendarNoteDTO> mappedNotes = new List<ReturnCalendarNoteDTO>();

            foreach (var note in notes)
            {
                mappedNotes.Add(_mapper.Map<ReturnCalendarNoteDTO>(note));
            }

            return mappedNotes;
        }

        ///<summary>
        ///Get notes for passed in month.
        ///</summary>
        [HttpGet("getNotesForMonth")]
        public async Task<ActionResult<List<ReturnCalendarNoteDTO>>> GetNotesForPassedMonth(GetNotesForMonthDTO getNotesForMonthDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var notes = await _apiHelper.ReturnNotesForMonth(getNotesForMonthDTO.FamilyId, id, getNotesForMonthDTO.Month);
                        
            if(notes == null) return BadRequest(new {
                errors = "You do not belong to this family."
            });

            List<ReturnCalendarNoteDTO> mappedNotes = new List<ReturnCalendarNoteDTO>();

            foreach (var note in notes)
            {
                mappedNotes.Add(_mapper.Map<ReturnCalendarNoteDTO>(note));
            }

            return mappedNotes;
        }
    
        [HttpPost]
        public async Task<ActionResult> AddVisit(AddVisitDTO addVisitDTO)
        {
            return Ok(addVisitDTO.Date);
        }

    }
}