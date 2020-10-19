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

            var isUserInThisFamily = await _apiHelper.DoesUserBelongToFamily(addCalendarNoteDTO.FamilyId, id);
            

            if(!isUserInThisFamily) return BadRequest(new {
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
                errors = "Invalid familyId or you do not belong to this family."
            });

            var visitToReturn = _mapper.Map<ReturnVisitDTO>(visit);

            return visitToReturn;
        }

        ///<summary>
        ///Get visits for actual month.
        ///</summary>
        [HttpGet("getLastVisits")]
        public async Task<ActionResult<List<ReturnVisitDTO>>> GetVisitsForCurrentMonth(GetNotesForActualMonthDTO getNotesForActualMonthDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var visits = await _apiHelper.ReturnCurrentMonthVisits(getNotesForActualMonthDTO.FamilyId, id);

            if(visits == null) return BadRequest(new {
                errors = "Invalid family id or you do not belong to this family."
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
        [HttpGet("getVisitsForMonth")]
        public async Task<ActionResult<List<ReturnVisitDTO>>> GetVisitsForPassedMonth(GetNotesForMonthDTO getNotesForMonthDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var visits = await _apiHelper.ReturnVisitsForMonth(getNotesForMonthDTO.FamilyId, id, getNotesForMonthDTO.Month);
                        
            if(visits == null) return BadRequest(new {
                errors = "You do not belong to this family."
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