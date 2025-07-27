using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp2_Iwankowski.Data
{
    public class StorageLocation
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa magazynu jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa magazynu nie mo¿e byæ d³u¿sza ni¿ 100 znaków")]
        public string Name { get; set; } = string.Empty;
        
        public List<WarehouseItem> Items { get; set; } = new();
    }

    public class WarehouseItem
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa produktu nie mo¿e byæ d³u¿sza ni¿ 100 znaków")]
        public string Name { get; set; } = string.Empty;
        
        [Range(1, int.MaxValue, ErrorMessage = "Iloœæ musi byæ wiêksza od zera")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi byæ wiêksza od zera")]
        public decimal Price { get; set; }
        
        public int? StorageLocationId { get; set; }
        public StorageLocation? StorageLocation { get; set; }
    }

    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        public DbSet<WarehouseItem> WarehouseItems => Set<WarehouseItem>();
        public DbSet<StorageLocation> StorageLocations => Set<StorageLocation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseItem>()
                .HasOne(w => w.StorageLocation)
                .WithMany(s => s.Items)
                .HasForeignKey(w => w.StorageLocationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<WarehouseItem>()
                .Property(w => w.Price)
                .HasColumnType("decimal(18,2)");
        }
    }

    public static class WarehouseDbContextExtensions
    {
        public static void EnsureDbCreated(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated(); 
        }
    }
}
