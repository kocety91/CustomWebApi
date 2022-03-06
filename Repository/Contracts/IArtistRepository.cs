using CustomWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Repository.Contracts
{
    public interface IArtistRepository : IRepositoryBase<Artist>
    {
        PagedList<Artist> GetAllArtists(ArtistParameters artistParameters);
        Task<Artist> GetArtisByIdAsync(int artistId);
        void CreateArtis(Artist artist);
        void UpdateArtis(Artist artist);
        void DeleteArtis(Artist artist);
    }
}
