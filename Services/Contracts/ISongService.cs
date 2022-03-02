using CustomWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomWebApi.Services.Contracts
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetAllSongsAsync();
    }
}
