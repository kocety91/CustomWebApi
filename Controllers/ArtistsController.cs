using AutoMapper;
using CustomWebApi.Dtos.Artists;
using CustomWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService,
            IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> GetById(int id)
        {
            var artist = await _artistService.GetByIdAsync(id);

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetAll()
        {
            var artists = await _artistService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ArtistDto>>(artists));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistDto>> Create(CreateArtistDto source)
        {
            var dtoToProduct = _mapper.Map<Artist>(source);
            await _artistService.CreateAsync(dtoToProduct);

            var redirectDto = _mapper.Map<ArtistDto>(dtoToProduct);

            return CreatedAtRoute(nameof(GetById), new { id = redirectDto.Id }, redirectDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> Update(int id, UpdateArtistDto source)
        {
            var artist = await _artistService.GetByIdAsync(id);

            _mapper.Map(source, artist);
            await _artistService.UpdateAsync(artist);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<UpdateArtistDto> patchDoc)
        {
            var artist = await _artistService.GetByIdAsync(id);


            var artistToPatch = _mapper.Map<UpdateArtistDto>(artist);
            patchDoc.ApplyTo(artistToPatch, ModelState);

            if (!TryValidateModel(artistToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(artistToPatch, artist);
            await _artistService.UpdateAsync(artist);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var artist = await _artistService.GetByIdAsync(id);

            await _artistService.DeleteAsync(artist);

            return NoContent();
        }

    }
}
