using CustomWebApi.Data;
using CustomWebApi.Repository.Contracts;

namespace CustomWebApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private CustomWebApiContext _repoContext;
        private IArtistRepository _artist;
        private ISongRepository _song;


        public RepositoryWrapper(CustomWebApiContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IArtistRepository Artist
        {
            get
            {
                if (_artist == null)
                {
                    _artist = new ArtistRepository(_repoContext);
                }
                return _artist;
            }
        }

        public ISongRepository Song
        {
            get
            {
                if (_song == null)
                {
                    _song = new SongRepository(_repoContext);
                }
                return _song;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
