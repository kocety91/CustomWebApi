using AutoMapper;
using CustomWebApi.Dtos.Artists;
using CustomWebApi.Models;

namespace CustomWebApi.Profiles
{
    public class ArtistsProfile : Profile
    {
        public ArtistsProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<CreateArtistDto, Artist>();
            CreateMap<UpdateArtistDto, Artist>();
            CreateMap<Artist, UpdateArtistDto>();
        }
    }
}
