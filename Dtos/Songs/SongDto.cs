using CustomWebApi.Dtos.Artists;

namespace CustomWebApi.Dtos.Songs
{
    public class SongDto
    {
        public string Name { get; set; }

        public ArtistDto Artist { get; set; }
    }
}
