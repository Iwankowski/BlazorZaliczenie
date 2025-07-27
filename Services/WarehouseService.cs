using BlazorApp2_Iwankowski.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2_Iwankowski.Services
{
    public class WarehouseService
    {
        private readonly WarehouseDbContext _context;
        public WarehouseService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseItem>> GetAllAsync()
            => await _context.WarehouseItems.ToListAsync();

        public async Task<WarehouseItem?> GetByIdAsync(int id)
            => await _context.WarehouseItems.FindAsync(id);

        public async Task AddAsync(WarehouseItem item)
        {
            _context.WarehouseItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WarehouseItem item)
        {
            _context.WarehouseItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.WarehouseItems.FindAsync(id);
            if (item != null)
            {
                _context.WarehouseItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
