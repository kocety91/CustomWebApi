using CustomWebApi.Dtos.Artists;
using System.ComponentModel.DataAnnotations;
using static CustomWebApi.Common.ErrorMessage.Song;

namespace CustomWebApi.Dtos.Songs
{
    public class UpdateSongDto
    {
        [Required(ErrorMessage = NameIsRequired)]
        [StringLength(24, MinimumLength = 2)]
        public string Name { get; set; }

        public UpdateArtistDto Artist { get; set; }
    }
}
