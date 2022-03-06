using CustomWebApi.Common;
using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CustomWebApi.Common.ErrorMessage.Song;

namespace CustomWebApi.Repository
{
    public class SongRepository : RepositoryBase<Song>, ISongRepository
    {
        public SongRepository(CustomWebApiContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void CreateSong(Song song)
        {
            CheckSong(song);

            Create(song);
        }

        public void DeleteSong(Song song)
        {
            Delete(song);
        }

        public async Task<IEnumerable<Song>> GetAllSongsAsync()
         => await FindAll().OrderBy(x => x.Id).ToListAsync();

        public async Task<Song> GetSongByIdAsync(int songId)
        {
            var song = await FindByCondition(x => x.Id == songId).FirstOrDefaultAsync();

            if (song == null)
            {
                throw new NotFoundException(SongDoesntExist);
            }

            return song;
        }

        public  void UpdateSong(Song song)
        {
            Update(song);
        }

        private void CheckSong(Song song)
        {
            if (song == null)
            {
                throw new NullReferenceException(nameof(song));
            }

            var songExists = FindAll()
               .FirstOrDefault(x => x.Id == song.Id);

            if (songExists != null)
            {
                throw new ArgumentException(SongAlreadyExist);
            }
        }
    }
}
