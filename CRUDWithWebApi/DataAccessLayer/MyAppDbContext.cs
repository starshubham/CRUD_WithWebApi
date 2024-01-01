using CRUDWithWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithWebApi.DataAccessLayer
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
