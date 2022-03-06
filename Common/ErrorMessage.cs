namespace CustomWebApi.Common
{
    public static class ErrorMessage
    {
        public static class Artist
        {
            public const string ArtistAlreadyExist = "Artist you want to add already exist.";

            public const string ArtistDoesntExist = "This artist doesn't exists.";

            public const string NoArtists = "No artists found.";

            public const string FirstNameIsRequired = "Artist firstname is required.";

            public const string LastNameIsRequired = "Artist lastname is required.";

        }

        public static class Song
        {
            public const string SongAlreadyExist = "Song you want to add already exists.";

            public const string SongDoesntExist = "This song doesn't exists.";

            public const string NoSongs = "No songs found.";

            public const string NameIsRequired = "Name is required.";

            public const string IvalidReleaseDate = "Min year of release must be less than max year of release";

        }
    }
}
