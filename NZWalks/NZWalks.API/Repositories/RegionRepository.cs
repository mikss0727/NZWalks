using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public RegionRepository(NzWalksDbContext nzWalksDbContext) // Constructor of DbContext "NzWalksDbContext"
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync() // implementation of IRegionRepository
        {
            return await nzWalksDbContext.Regions.ToListAsync();
        }
    }
}
