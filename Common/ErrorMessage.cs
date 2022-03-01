﻿namespace CustomWebApi.Common
{
    public static class ErrorMessage
    {
        public static class Artist
        {
            public const string ArtistAlreadyExist = "Artist you want to add already exist";

            public const string ArtistDoesntExist = "This artist doesn't exists";

            public const string NoArtists = "No artists found.";
        }
    }
}