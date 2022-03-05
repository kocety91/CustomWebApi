using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;

namespace CustomWebApi.Repository
{
    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(CustomWebApiContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
