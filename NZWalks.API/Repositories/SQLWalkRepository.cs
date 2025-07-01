using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            // Retrieve all walks from the database asynchronously
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skips = (pageNumber - 1) * pageSize;

            return await walks.Skip(skips).Take(pageSize).ToListAsync();
        }
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            // Retrieve a walk by its ID from the database asynchronously
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null; // Return null if the walk does not exist
            }
            existingWalk.Name = walk.Name; // Update the properties of the existing walk
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId; // Update the DifficultyId
            existingWalk.RegionId = walk.RegionId; // Update the DifficultyId

            await dbContext.SaveChangesAsync(); // Save changes to the database
            return existingWalk; // Return the updated walk
        }
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null; // Return null if the walk does not exist
            }
            dbContext.Walks.Remove(existingWalk); // Remove the existing walk from the database
            await dbContext.SaveChangesAsync(); // Save changes to the database
            return existingWalk; // Return the deleted walk
        }
    }
}