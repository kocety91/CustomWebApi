using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CustomWebApi.Common.ErrorMessage.Song;

namespace CustomWebApi.Services
{
    public class SongService : ISongService
    {
        private readonly CustomWebApiContext _context;

        public SongService(CustomWebApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            var songs = await _context.Songs.Include(s => s.Artist).AsNoTracking().ToListAsync();

            if ( songs == null)
            {
                throw new ArgumentException(NoSongs);
            }

            return songs;
        }

        public async Task<IEnumerable<Song>> GetSongsByArtistIdAsync(int artistId)
        {
            var songs = await _context.Songs.Where(x => x.ArtistId == artistId)
                .Include(x => x.Artist)
                .ToListAsync();

            if(songs == null)
            {
                throw new ArgumentException(NoSongs);
            }

            return songs;
        }
    }
}
