using CustomWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Repository.Contracts
{
    public interface ISongRepository : IRepositoryBase<Song>
    {
        PagedList<Song> GetAllSongs(SongParameters songParameters);
        Task<Song> GetSongByIdAsync(int songId);
        void CreateSong(Song song);
        void UpdateSong(Song song);
        void DeleteSong(Song song);
    }
}
