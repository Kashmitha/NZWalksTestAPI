using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.dbContext = nZWalksDbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk); // Add the new walk to the database asynchronously
            await dbContext.SaveChangesAsync(); // Save changes to the database
            return walk; // Return the newly created walk
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            // Retrieve all walks from the database asynchronously
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}
