using CustomWebApi.Dtos.Artists;
using System;

namespace CustomWebApi.Dtos.Songs
{
    public class SongDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public SongArtistDto Artist { get; set; }
    }
}
