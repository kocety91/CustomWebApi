using CustomWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomWebApi.Data
{
    public class CustomWebApiContext : DbContext
    {
        public CustomWebApiContext(DbContextOptions<CustomWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Songs)
                .WithOne(s => s.Artist)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
