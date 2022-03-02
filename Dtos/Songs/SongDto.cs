using CustomWebApi.Dtos.Artists;

namespace CustomWebApi.Dtos.Songs
{
    public class SongDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SongArtistDto Artist { get; set; }
    }
}
