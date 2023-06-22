using Microsoft.EntityFrameworkCore;
using WebApiCore.Models;

namespace WebApiCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 

        }        
        public DbSet<Customers> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Nlogs> Nlogs { get; set; }
    }
}
