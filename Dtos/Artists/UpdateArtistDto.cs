using System.ComponentModel.DataAnnotations;
using static CustomWebApi.Common.ErrorMessage.Artist;

namespace CustomWebApi.Dtos.Artists
{
    public class UpdateArtistDto
    {
        [Required(ErrorMessage = FirstNameIsRequired)]
        [StringLength(12, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = LastNameIsRequired)]
        [StringLength(12, MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
