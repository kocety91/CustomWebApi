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

        public async Task CreateAsync(Song song)
        {
           if (song == null)
           {
               throw new NullReferenceException(nameof(song));
           }

           var songExist = await _context.Songs.FirstOrDefaultAsync(x => x.Name == song.Name);

           if (songExist != null)
           {
                throw new ArgumentException(SongAlreadyExists);
           }

            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
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

        public async Task<Song> GetByIdAsync(int id)
        {
            var song = await _context.Songs.Include(s => s.Artist).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(song == null)
            {
                throw new ArgumentException(SongDoesntExists);
            }

            return song;
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

        public async Task UpdateAsync(Song song)
        {
            if (song == null)
            {
                throw new NullReferenceException(nameof(song));
            }

            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
        }
    }
}
