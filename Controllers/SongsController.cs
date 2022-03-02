using AutoMapper;
using CustomWebApi.Dtos.Songs;
using CustomWebApi.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songsServerice;
        private readonly IMapper _mapper;

        public SongsController(ISongService songsServerice,
            IMapper mapper)
        {
            _songsServerice = songsServerice;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SongDto>>> GetAll()
        {
            var songs = await _songsServerice.GetAllSongsAsync();

            return Ok(_mapper.Map<IEnumerable<SongDto>>(songs));
        }


        [HttpGet("{artistId}", Name = "GetSongsByArtistId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SongDto>> GetSongsByArtistId(int artistId)
        {
            var songs = await _songsServerice.GetSongsByArtistIdAsync(artistId);

            return Ok(_mapper.Map<IEnumerable<SongDto>>(songs));
        }
    }
}
