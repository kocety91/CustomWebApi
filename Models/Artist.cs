using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWebApi.Models
{
    public class Artist
    {
        public Artist()
        {
            this.Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
