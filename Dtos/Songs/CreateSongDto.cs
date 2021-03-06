using CustomWebApi.Dtos.Artists;
using System;
using System.ComponentModel.DataAnnotations;
using static CustomWebApi.Common.ErrorMessage.Song;

namespace CustomWebApi.Dtos.Songs
{
    public class CreateSongDto
    {
        [Required(ErrorMessage = NameIsRequired)]
        [StringLength(24, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }


        public CreateArtistDto Artist { get; set; }
    }
}
