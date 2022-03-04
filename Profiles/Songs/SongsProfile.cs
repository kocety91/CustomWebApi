using AutoMapper;
using CustomWebApi.Dtos.Songs;
using CustomWebApi.Models;

namespace CustomWebApi.Profiles.Songs
{
    public class SongsProfile : Profile
    {
        public SongsProfile()
        {
            CreateMap<Song, SongDto>();
            CreateMap<CreateSongDto, Song>();
            CreateMap<UpdateSongDto, Song>();
            CreateMap<Song, UpdateSongDto>();
        }
    }
}
