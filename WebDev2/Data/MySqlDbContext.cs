using Microsoft.EntityFrameworkCore;
using WebDev2.Models;
namespace WebDev2.Data
{
    public class MySqlDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Account { get; set; }
        //public DbSet<Person> Persons { get; set; }
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { 

        }
    }
}
