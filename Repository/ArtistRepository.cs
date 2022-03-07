using CustomWebApi.Common;
using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CustomWebApi.Common.ErrorMessage.Artist;

namespace CustomWebApi.Repository
{
    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(CustomWebApiContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void CreateArtis(Artist artist)
        {
            CheckArtist(artist);

            Create(artist);
        }

        public void DeleteArtis(Artist artist)
        {
            Delete(artist);
        }

        public PagedList<Artist> GetAllArtists(ArtistParameters artistParameters)
        {
            return PagedList<Artist>.ToPagedList(FindAll().OrderBy(x => x.Id)
            .Include(x => x.Songs), artistParameters.PageNumber,
            artistParameters.PageSize);
        } 

        public async Task<Artist> GetArtisByIdAsync(int artistId)
        {
            var artist = await FindByCondition(x => x.Id == artistId).Include(x => x.Songs).FirstOrDefaultAsync();

            if (artist == null)
            {
                throw new NotFoundException(ArtistDoesntExist);
            }

            return artist;
        }

        public void UpdateArtis(Artist artist)
        {
            Update(artist);
        }
        
        private void CheckArtist(Artist artist)
        {
            if (artist == null)
            {
                throw new NullReferenceException(nameof(artist));
            }

            var artistExists = FindAll()
                .FirstOrDefault(x => x.FirstName == artist.FirstName
                && x.LastName == artist.LastName);

            if (artistExists != null)
            {
                throw new ArgumentException(ArtistAlreadyExist);
            }
        }

    }
}
