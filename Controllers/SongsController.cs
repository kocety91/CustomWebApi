using AutoMapper;
using CustomWebApi.Dtos.Songs;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public SongsController(IRepositoryWrapper repoWrapper,
            IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll([FromQuery] SongParameters songParameters)
        {
            var songs = _repoWrapper.Song.GetAllSongs(songParameters);

            var metadata = new
            {
                songs.TotalCount,
                songs.PageSize,
                songs.CurrentPage,
                songs.TotalPages,
                songs.HasNext,
                songs.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(songs);
        }


        [HttpGet("{id}", Name = "GetSongById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SongDto>> GetSongById(int id)
        {
            var songs = await _repoWrapper.Song.GetSongByIdAsync(id);

            return Ok(_mapper.Map<SongDto>(songs));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SongDto>> Create(CreateSongDto source)
        {
            var dtoToSong = _mapper.Map<Song>(source);
            _repoWrapper.Song.CreateSong(dtoToSong);
            await _repoWrapper.SaveAsync();

            var redirectDto = _mapper.Map<SongDto>(dtoToSong);

            return CreatedAtRoute(nameof(GetSongById), new { id = redirectDto.Artist.Id }, redirectDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateSongDto source)
        {
            var songForUpdate = await _repoWrapper.Song.GetSongByIdAsync(id);

            if (songForUpdate == null)
            {
                return this.NotFound();
            }

            _mapper.Map(source, songForUpdate);
            _repoWrapper.Song.UpdateSong(songForUpdate);
            await _repoWrapper.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<UpdateSongDto> patchDoc)
        {
            var song = await _repoWrapper.Song.GetSongByIdAsync(id);

            if (song == null)
            {
                return this.NotFound();
            }

            var songToPatch = _mapper.Map<UpdateSongDto>(song);
            patchDoc.ApplyTo(songToPatch, ModelState);

            if (!TryValidateModel(songToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(songToPatch, song);
            _repoWrapper.Song.UpdateSong(song);
            await _repoWrapper.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var song = await _repoWrapper.Song.GetSongByIdAsync(id);

            _repoWrapper.Song.DeleteSong(song);
            await _repoWrapper.SaveAsync();

            return NoContent();
        }
    }
}
