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
        [HttpPost("addNote")]
        public async Task<ActionResult<ReturnCalendarNoteDTO>> AddCalendarNote(AddCalendarNoteDTO addCalendarNoteDTO) 
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var mappedNote = _mapper.Map<CalendarNote>(addCalendarNoteDTO);
            
            var calendarNote = await _apiHelper.AddCalendarNote(mappedNote, id);

            if(calendarNote == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny."
            });

            return _mapper.Map<ReturnCalendarNoteDTO>(calendarNote);
        }

        ///<summary>
        ///Get notes from current month by token
        ///</summary>
        [HttpGet("getLastNotes/{familyId}")]
        public async Task<ActionResult<List<ReturnCalendarNoteDTO>>> GetLastCalendarNotes(string familyId)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var notes = await _apiHelper.ReturnCurrentMonthNotes(familyId, id);

            if(notes == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny."
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
        [HttpGet("getNotesForMonth/{familyId}/{month}")]
        public async Task<ActionResult<List<ReturnCalendarNoteDTO>>> GetNotesForPassedMonth(string familyId, string month)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var notes = await _apiHelper.ReturnNotesForMonth(familyId, id, month);
                        
            if(notes == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny."
            });

            List<ReturnCalendarNoteDTO> mappedNotes = new List<ReturnCalendarNoteDTO>();

            foreach (var note in notes)
            {
                mappedNotes.Add(_mapper.Map<ReturnCalendarNoteDTO>(note));
            }

            return mappedNotes;
        }
    
        ///<summary>
        ///Add visit to calendar.
        ///</summary>
        [HttpPost("addVisit")]
        public async Task<ActionResult<ReturnVisitDTO>> AddVisit(AddVisitDTO addVisitDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var visitToAdd = _mapper.Map<Visit>(addVisitDTO);
            var visit = await _apiHelper.AddVisit(visitToAdd, id);

            if(visit == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny lub nie jesteś psychologiem."
            });

            var visitToReturn = _mapper.Map<ReturnVisitDTO>(visit);

            return visitToReturn;
        }

        ///<summary>
        ///Get visits for actual month.
        ///</summary>
        [HttpGet("getLastVisits/{familyId}")]
        public async Task<ActionResult<List<ReturnVisitDTO>>> GetVisitsForCurrentMonth(string familyId)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var visits = await _apiHelper.ReturnCurrentMonthVisits(familyId, id);

            if(visits == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny."
            });

            List<ReturnVisitDTO> mappedVisits = new List<ReturnVisitDTO>();

            foreach (var visit in visits)
            {
                mappedVisits.Add(_mapper.Map<ReturnVisitDTO>(visit));
            }

            return mappedVisits;
        }

        ///<summary>
        ///Get visits for passed month.
        ///</summary>
        [HttpGet("getVisitsForMonth/{familyId}/{month}")]
        public async Task<ActionResult<List<ReturnVisitDTO>>> GetVisitsForPassedMonth(string familyId, string month)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var visits = await _apiHelper.ReturnVisitsForMonth(familyId, id, month);
                        
            if(visits == null) return BadRequest(new {
                errors = "Niepoprawne id rodziny."
            });

            List<ReturnVisitDTO> mappedVisits = new List<ReturnVisitDTO>();

            foreach (var visit in visits)
            {
                mappedVisits.Add(_mapper.Map<ReturnVisitDTO>(visit));
            }

            return mappedVisits;
        }
    }
}