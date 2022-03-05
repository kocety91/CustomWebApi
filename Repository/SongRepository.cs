using CustomWebApi.Data;
using CustomWebApi.Models;
using CustomWebApi.Repository.Contracts;

namespace CustomWebApi.Repository
{
    public class SongRepository : RepositoryBase<Song>, ISongRepository
    {
        public SongRepository(CustomWebApiContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
