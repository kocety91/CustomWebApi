using CustomWebApi.Dtos.Songs;
using System.Collections.Generic;

namespace CustomWebApi.Dtos.Artists
{
    public class ArtistDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<SongDto> Songs { get; set; }
    }
}
