using CustomWebApi.Dtos.Artists;
using CustomWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApi.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetById(int id)
        {
            var artist = await _artistService.GetByIdAsync(id);


        }


       
    }
}
