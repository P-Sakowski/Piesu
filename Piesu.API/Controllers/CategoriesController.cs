using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return categoryModel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDetails input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(input);
            _context.Entry(category).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Category {input.Name} updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDetails>> PostCategory(CategoryDetails input)
        {
            var category = _mapper.Map<Category>(input);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Category {input.Name} created successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Category {categoryModel.Name} deleted successfully" });
        }
    }
}
