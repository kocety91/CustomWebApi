using AutoMapper;
using CustomWebApi.Dtos.Artists;
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
    [Route("api/artists")]
    public class ArtistsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public ArtistsController(IRepositoryWrapper repoWrapper,
            IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> GetById(int id)
        {
            var artist = await _repoWrapper.Artist.GetArtisByIdAsync(id);

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAll([FromQuery] ArtistParameters artistParameters)
        {
            var artists =  _repoWrapper.Artist.GetAllArtists(artistParameters);

            var metadata = new
            {
                artists.TotalCount,
                artists.PageSize,
                artists.CurrentPage,
                artists.TotalPages,
                artists.HasNext,
                artists.HasPrevious
            };

            var artistsToDto = _mapper.Map<IEnumerable<ArtistDto>>(artists);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));



            return Ok(artistsToDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistDto>> Create(CreateArtistDto source)
        {
            var dtoToProduct = _mapper.Map<Artist>(source);

            _repoWrapper.Artist.CreateArtis(dtoToProduct);
            await _repoWrapper.SaveAsync();

            var redirectDto = _mapper.Map<ArtistDto>(dtoToProduct);

            return CreatedAtRoute(nameof(GetById), new { id = redirectDto.Id }, redirectDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> Update(int id, UpdateArtistDto source)
        {
            var artist = await _repoWrapper.Artist.GetArtisByIdAsync(id);

            if (artist == null)
            {
                return this.NotFound();
            }

            _mapper.Map(source, artist);
            _repoWrapper.Artist.UpdateArtis(artist);
            await _repoWrapper.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<UpdateArtistDto> patchDoc)
        {
            var artist = await _repoWrapper.Artist.GetArtisByIdAsync(id);

            var artistToPatch = _mapper.Map<UpdateArtistDto>(artist);
            patchDoc.ApplyTo(artistToPatch, ModelState);

            if (!TryValidateModel(artistToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(artistToPatch, artist);
            _repoWrapper.Artist.Update(artist);
            await _repoWrapper.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var artist = await _repoWrapper.Artist.GetArtisByIdAsync(id);

            _repoWrapper.Artist.DeleteArtis(artist);
            await _repoWrapper.SaveAsync();

            return NoContent();
        }

    }
}
