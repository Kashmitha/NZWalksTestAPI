using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for the difficulties table (Easy, Medium, Hard)    
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("7216fc77-5d75-4bf4-b874-64f8fcc2ada8"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("682f7013-ff9b-435d-876a-23ae3ec3795e"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("99ac3a26-df48-4f01-8f0b-d7954cd2e9e0"),
                    Name = "Hard"
                }
            };

            // Seed the difficulties data into the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for the regions table
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("3be7090d-a168-46ae-a2f1-8ba1fb06407c"),
                    Name = "Northland",
                    Code = "NTH",
                    RegionImageUrl = "https://example.com/northland.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("fbfed9f1-fdee-4ccc-826e-fcf0799c767a"),
                    Name = "Southland",
                    Code = "STH",
                    RegionImageUrl = "https://example.com/southland.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("72c44ed3-7203-4d17-adaf-96c9027a6906"),
                    Name = "Westland",
                    Code = "WST",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("8d98bcb8-6671-4ca6-8712-9267383f903b"),
                    Name = "Eastland",
                    Code = "EST",
                    RegionImageUrl = "https://example.com/eastland.jpg"
                }
            };

            // Seed the regions data into the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
