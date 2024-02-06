using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piesu.API.Data;
using Piesu.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public SubcategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategory>>> GetSubcategories()
        {
            return await _context.Subcategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategory>> GetSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);

            if (subcategory == null)
            {
                return NotFound();
            }

            return subcategory;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategory(int id, SubcategoryDetails input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Subcategory>(input);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Subcategory {input.Name} updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<Subcategory>> PostSubcategory(SubcategoryDetails input)
        {
            var model = _mapper.Map<Subcategory>(input);
            _context.Subcategories.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Subcategory {input.Name} created successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }

            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Subcategory {subcategory.Name} deleted successfully" });
        }
    }
}
