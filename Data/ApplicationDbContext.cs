using Microsoft.EntityFrameworkCore;
using ManageAndStorage.Models;

namespace ManageAndStorage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        
        public DbSet<BussinessType> BussinessTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}