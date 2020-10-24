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
    public class PrivateNotesController : ControllerBase
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        public PrivateNotesController(IApiHelper apiHelper, ITokenHelper tokenHelper, IMapper mapper)
        {
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _apiHelper = apiHelper;
        }

        ///<summary>
        /// Add private note, only user notes.
        ///</summary>
        [HttpPost("add")]
        public async Task<ActionResult<ReturnPrivateNoteDTO>> AddPrivateNote(AddPrivateNoteDTO addPrivateNoteDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var note = await _apiHelper.AddPrivateNote(addPrivateNoteDTO.Message, id);

            return _mapper.Map<ReturnPrivateNoteDTO>(note);
        }

        ///<summary>
        /// Get by token all user notes.
        ///</summary>
        [HttpGet("getNotes")]
        public async Task<ActionResult<List<ReturnPrivateNoteDTO>>> ReturnUserNotes()
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            List<PrivateNote> notes = await _apiHelper.ReturnUserPrivateNotes(id);
            List<ReturnPrivateNoteDTO> notesToReturn = new List<ReturnPrivateNoteDTO>();

            for (int i = 0; i < notes.Count; i++)
            {
                notesToReturn.Add(_mapper.Map<ReturnPrivateNoteDTO>(notes[i]));
            }

            return notesToReturn;
        }

        ///<summary>
        /// Update user note.
        ///</summary>
        [HttpPut("update")]
        public async Task<ActionResult<ReturnPrivateNoteDTO>> UpdateNote(UpdateNoteDTO updateNoteDTO)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var note = await _apiHelper.UpdateNote(updateNoteDTO.Message, updateNoteDTO.NoteId, id);

            if(note == null) return BadRequest(new {
                errors = "Niepoprawne id notatki."
            });

            return _mapper.Map<ReturnPrivateNoteDTO>(note);
        }

        ///<summary>
        /// Delete user's private note.
        ///</summary>
        [HttpDelete("delete/{noteId}")]
        public async Task<IActionResult> DeleteNote(string noteId)
        {
            var id = _tokenHelper.GetIdByToken(HttpContext.Request.Headers["Authorization"]);

            var isNoteDeleted = await _apiHelper.DeletePrivateNote(noteId, id);

            if(!isNoteDeleted) return BadRequest(new {
                errors = "Złe id notatki."
            });
            else return Ok(new {
                message = "Notatka została usunięta."
            });
        }
    }
}