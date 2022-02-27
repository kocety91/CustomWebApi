using System.ComponentModel.DataAnnotations;

namespace CustomWebApi.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
