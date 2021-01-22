using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_web_api.Data;
using CRUD_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_web_api.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    [ResponseCache(Duration = 60)]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        private bool CategoryExists(int id) =>
            _context.Categories.Any(x => x.Id == id);

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet]
        [Route("{id}/products")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(int id)
        {
            var products = await _context.Products.Where(x => x.CategoryId == id).ToListAsync();
            
            if (products == null)
                return NotFound();

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                
                return category;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Category>> PutCategory(int id, [FromBody]Category category)
        {
            if (id != category.Id)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return category;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                    return NotFound();
                else
                    throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
