using AutoMapper;
using CustomWebApi.Dtos.Songs;
using CustomWebApi.Models;
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


        [HttpGet("{id}", Name = "GetSongsByArtistId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SongDto>> GetSongsByArtistId(int id)
        {
            var songs = await _songsServerice.GetSongsByArtistIdAsync(id);

            return Ok(_mapper.Map<IEnumerable<SongDto>>(songs));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SongDto>> Create(CreateSongDto source)
        {
            var dtoToSong = _mapper.Map<Song>(source);
            await _songsServerice.CreateAsync(dtoToSong);

            var redirectDto = _mapper.Map<SongDto>(dtoToSong);

            return CreatedAtRoute(nameof(GetSongsByArtistId), new { id = redirectDto.Artist.Id }, redirectDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(int id, UpdateSongDto source)
        {
            var songForUpdate = await _songsServerice.GetByIdAsync(id);

            _mapper.Map(source, songForUpdate);
            await _songsServerice.UpdateAsync(songForUpdate);

            return NoContent();
        }
    }
}
