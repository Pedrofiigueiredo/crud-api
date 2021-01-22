using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_web_api.Data;
using CRUD_web_api.Models;
using CRUD_web_api.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_web_api.Controllers
{
  [ApiController]
  [Route("v1/products")]
  public class ProductController : ControllerBase
  {
    private readonly DataContext _context;

    public ProductController(DataContext context)
    {
      _context = context;
    }

    private bool ProductExists(int id) =>
      _context.Products.Any(x => x.Id == id);

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<ListProductViewModel>>> GetProducts()
    {
      var products = await _context
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

      return products;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
      var product = await _context.Products.FindAsync(id);
      
      if (product == null)
        return NotFound();

      return product;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<ResultViewModel>> PostProduct([FromBody]EditorProductViewModel model)
    {
      model.Validate();
      if (model.Invalid)
        return new ResultViewModel
        {
          Result = false,
          Message = "Não foi possível criar o produto",
          Data = model.Notifications
        };

      var product = new Product();
      product.Title = model.Title;
      product.Description = model.Description;
      product.Price = model.Price;
      product.Quantity = model.Quantity;
      product.Image = model.Image;
      product.CreateDate = DateTime.Now;
      product.LastUpdateDate = DateTime.Now;
      product.CategoryId = model.CategoryId;

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return new ResultViewModel
      {
        Result = true,
        Message = "Produto adicionado com sucesso",
        Data = product
      };
    }

    [HttpPut]
    [Route("")]
    public async Task<ActionResult<ResultViewModel>> PutProduct([FromBody]EditorProductViewModel model)
    {
      model.Validate();
      if (model.Invalid)
        return new ResultViewModel
        {
          Result = false,
          Message = "Não foi possível alterar o produto",
          Data = model.Notifications
        };

      var product = await _context.Products.FindAsync(model.Id);
      product.Title = model.Title;
      product.Description = model.Description;
      product.Price = model.Price;
      product.Quantity = model.Quantity;
      product.Image = model.Image;
      /* product.CreateDate = DateTime.Now; */
      product.LastUpdateDate = DateTime.Now;
      product.CategoryId = model.CategoryId;

      _context.Entry(product).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return new ResultViewModel
      {
        Result = true,
        Message = "Produto alterado com sucesso",
        Data = product
      };
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
        return NotFound();

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return product;
    }

  }
}