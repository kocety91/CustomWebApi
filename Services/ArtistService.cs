using CustomWebApi.Common;
using CustomWebApi.Data;
using CustomWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CustomWebApi.Common.ErrorMessage.Artist;

namespace CustomWebApi.Services
{
    public class ArtistService : IArtistService
    {
        private readonly CustomWebApiContext _context;

        public ArtistService(CustomWebApiContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Artist artist)
        {
           if (artist == null)
           {
                throw new NullReferenceException(nameof(artist));
           }

            var artistExists = await _context.Artists
                .FirstOrDefaultAsync(x => x.FirstName == artist.FirstName
                && x.LastName == artist.LastName);

           if (artistExists !=  null)
           {
                throw new ArgumentException(ArtistAlreadyExist);
           }

           await _context.Artists.AddAsync(artist);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Artist artist)
        {
            var deleteArtist = await _context.Artists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == artist.Id);

            if (deleteArtist == null)
            {
                throw new ArgumentException(ArtistDoesntExist);
            }

            _context.Artists.Remove(deleteArtist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            var allArtists = await _context.Artists
                .Include(x => x.Songs)
                .AsNoTracking()
                .ToListAsync();

            if (allArtists == null)
            {
                throw new ArgumentException(NoArtists);
            }

            return allArtists;
        }
         
        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await _context.Artists
                .Include(x => x.Songs)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (artist == null)
            {
                throw new NotFoundException(ArtistDoesntExist);
            }

            return artist;
        }

        public async Task UpdateAsync(Artist artist)
        {
            if (artist == null)
            {
                throw new NullReferenceException(ArtistDoesntExist);
            }

            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }
    }
}
