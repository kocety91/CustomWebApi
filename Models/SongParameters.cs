using System;

namespace CustomWebApi.Models
{
    public class SongParameters : QueryStringParameters
    {
        public uint MinYearOfRelease { get; set; }
        public uint MaxYearOfRelease { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRange => MaxYearOfRelease > MinYearOfRelease;

        public string ArtistFirstName { get; set; }
    }
}
