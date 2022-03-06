using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;
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

        public Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Song> GetSongByIdAsync(int songId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSong(Song song)
        {
            throw new System.NotImplementedException();
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
