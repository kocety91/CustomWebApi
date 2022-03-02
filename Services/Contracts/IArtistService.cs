﻿using CustomWebApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CustomWebApi.Services
{
    public interface IArtistService
    {
        Task<Artist> GetByIdAsync(int id);

        Task<IEnumerable<Artist>> GetAllAsync();

        Task CreateAsync(Artist artist);

        Task UpdateAsync(Artist artist);

        Task DeleteAsync(Artist artist);
    }
}