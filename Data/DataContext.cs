using Microsoft.EntityFrameworkCore;
using CRUD_web_api.Models;

namespace CRUD_web_api.Data
{
  public class DataContext : DbContext
  {
     public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
  }
}