using BlazorApp2_Iwankowski.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2_Iwankowski.Services
{
    public class StorageLocationService
    {
        private readonly WarehouseDbContext _context;

        public StorageLocationService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<StorageLocation>> GetAllAsync()
            => await _context.StorageLocations.Include(s => s.Items).ToListAsync();

        public async Task<StorageLocation?> GetByIdAsync(int id)
            => await _context.StorageLocations.FindAsync(id);

        public async Task<StorageLocation> AddAsync(StorageLocation location)
        {
            _context.StorageLocations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task UpdateAsync(StorageLocation location)
        {
            _context.StorageLocations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _context.StorageLocations.FindAsync(id);
            if (location != null)
            {
                _context.StorageLocations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}