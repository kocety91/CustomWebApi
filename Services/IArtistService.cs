using CustomWebApi.Models;
using System.Threading.Tasks;

namespace CustomWebApi.Services
{
    public interface IArtistService
    {
        Task<Artist> GetByIdAsync(int id);

    }
}
