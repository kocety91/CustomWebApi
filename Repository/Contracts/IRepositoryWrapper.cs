using System.Threading.Tasks;

namespace CustomWebApi.Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IArtistRepository Artist { get; }
        ISongRepository Song { get; }
        Task SaveAsync();
    }
}
