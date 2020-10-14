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
        /// <param name="addPrivateNoteDTO">Input object</param>
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
    }
}