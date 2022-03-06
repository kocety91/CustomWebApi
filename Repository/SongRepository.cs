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

        public PagedList<Song> GetAllSongs(SongParameters songParameters)
        {
            return PagedList<Song>.ToPagedList(FindAll().OrderBy(x => x.Id)
            .Include(x => x.Artist), songParameters.PageNumber,
            songParameters.PageSize);
        }

        public async Task<Song> GetSongByIdAsync(int songId)
        {
            var song = await FindByCondition(x => x.Id == songId).Include(x => x.Artist).FirstOrDefaultAsync();

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
