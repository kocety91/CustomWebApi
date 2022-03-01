using CustomWebApi.Data;
using CustomWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApi.Services
{
    public class ArtistService : IArtistService
    {
        private readonly CustomWebApiContext _context;

        public ArtistService(CustomWebApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (artist == null)
            {
                throw new ArgumentException();
            }

            return artist;
        }


    }
}
