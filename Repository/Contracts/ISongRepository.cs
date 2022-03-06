using CustomWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Repository.Contracts
{
    public interface ISongRepository : IRepositoryBase<Song>
    {
        Task<IEnumerable<Song>> GetAllSongsAsync();
        Task<Song> GetSongByIdAsync(int songId);
        void CreateSong(Song song);
        void UpdateSong(Song song);
        void DeleteSong(Song song);
    }
}
