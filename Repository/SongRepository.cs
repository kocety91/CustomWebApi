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
            var songs = FindByCondition(s => s.ReleaseDate.Year >= songParameters.MinYearOfRelease &&
                 s.ReleaseDate.Year <= songParameters.MaxYearOfRelease);

            SearchByArtistName(ref songs, songParameters.ArtistFirstName);

            return PagedList<Song>.ToPagedList(songs.OrderBy(s => s.ReleaseDate).Include(s => s.Artist), songParameters.PageNumber,
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

        private void SearchByArtistName(ref IQueryable<Song> songs, string artistFirstName)
        {
            if (!songs.Any(x => x.Artist.FirstName == artistFirstName) || string.IsNullOrWhiteSpace(artistFirstName))
                return;

            songs = songs.Where(s => s.Artist.FirstName.ToLower().Contains(artistFirstName.Trim().ToLower()));
        }
    }
}
