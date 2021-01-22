using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_web_api.Data;
using CRUD_web_api.Models;
using CRUD_web_api.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_web_api.Repository
{
  public class ProductRepository
  {
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<ActionResult<List<ListProductViewModel>>> Get()
    {
      return await _context
        .Products
        .Include(x => x.Category)
        .Select(x => new ListProductViewModel
        {
          Id = x.Id,
          Title = x.Title,
          Price = x.Price,
          Category = x.Category.Title,
          CategoryId = x.Category.Id,
        })
        .ToListAsync();
    }

    public async void Save(Product product)
    {
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async void Update(Product product)
    {
      _context.Entry(product).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public async void Delete(Product product)
    {
      _context.Products.Remove(product);
      await _context.SaveChangesAsync();
    }
  }
}