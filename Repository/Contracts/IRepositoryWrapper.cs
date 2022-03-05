namespace CustomWebApi.Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IArtistRepository Artist { get; }
        ISongRepository Song { get; }
        void Save();
    }
}
