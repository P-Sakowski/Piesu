using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Piesu.API.Data;
using Piesu.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Piesu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        protected readonly IMapper _mapper;

        public AdvertsController(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthController> logger,
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._context = context;
            this._httpContext = httpContextAccessor;
            this._mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<AdvertDto> GetAdverts()
        {
            var dbAdverts = _context.Adverts.Where(advert => advert.Status == Enums.AdvertStatus.Approved).ToList();
            var adverts = new List<AdvertDto>();
            foreach (var dbAdvert in dbAdverts)
            {
                var advert = _mapper.Map<AdvertDto>(dbAdvert);
                var category = _mapper.Map<CategoryDto>(dbAdvert.Category);
                var subcategory = _mapper.Map<SubcategoryDto>(dbAdvert.Subcategory);
                advert.Category = category;
                advert.Subcategory = subcategory;
                adverts.Add(advert);
            }
            return adverts;
        }

        [HttpGet]
        [Route("unmoderated")]
        public IEnumerable<AdvertDto> GetUnmoderatedAdverts()
        {
            var dbAdverts = _context.Adverts.Where(advert => advert.Status == Enums.AdvertStatus.Unverified).ToList();
            var adverts = new List<AdvertDto>();
            foreach (var dbAdvert in dbAdverts)
            {
                var advert = _mapper.Map<AdvertDto>(dbAdvert);
                var category = _mapper.Map<CategoryDto>(dbAdvert.Category);
                var subcategory = _mapper.Map<SubcategoryDto>(dbAdvert.Subcategory);
                advert.Category = category;
                advert.Subcategory = subcategory;
                adverts.Add(advert);
            }
            return adverts;
        }

        [HttpGet("{id}")]
        public AdvertDto GetAdvert(int id)
        {
            var dbAdvert = _context.Adverts.Find(id);

            if (dbAdvert == null)
            {
                return null;
            }
            var advert = _mapper.Map<AdvertDto>(dbAdvert);
            var category = _mapper.Map<CategoryDto>(dbAdvert.Category);
            var subcategory = _mapper.Map<SubcategoryDto>(dbAdvert.Subcategory);
            advert.Category = category;
            advert.Subcategory = subcategory;
            return advert;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvert(int id, AdvertDetails input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var advert = _context.Adverts.Find(id);
            advert.Title = input.Title;
            advert.Description = input.Description;
            advert.Price = input.Price;
            advert.IsNegotiable = input.IsNegotiable;
            advert.Category = _context.Categories.Find(input.CategoryId);
            advert.Subcategory = _context.Subcategories.Find(input.SubcategoryId);
            advert.Location = _context.Locations.Find(input.LocationId);
            advert.CreateDate = DateTime.Now;
            advert.Status = Enums.AdvertStatus.Unverified;

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert updated successfully" });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Advert>> PostAdvert(AdvertDetails input)
        {
            var userEmail = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid user." });
            }

            var advert = new Advert
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                Price = input.Price,
                IsNegotiable = input.IsNegotiable,
                Category = _context.Categories.Find(input.CategoryId),
                Subcategory = _context.Subcategories.Find(input.SubcategoryId),
                Location = _context.Locations.Find(input.LocationId),
                CreateDate = DateTime.Now,
                Status = Enums.AdvertStatus.Unverified,
                Author = user
            };
            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert created successfully" });
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/approve")]
        public async Task<ActionResult<Advert>> Approve(int id)
        {
            var advert = _context.Adverts.Find(id);
            advert.Status = Enums.AdvertStatus.Approved;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert approved successfully" });
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/deactivate")]
        public async Task<ActionResult<Advert>> Deactivate(int id)
        {
            var advert = _context.Adverts.Find(id);
            advert.Status = Enums.AdvertStatus.Deleted;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert approved successfully" });
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/close")]
        public async Task<ActionResult<Advert>> Close(int id)
        {
            var advert = _context.Adverts.Find(id);
            advert.Status = Enums.AdvertStatus.Closed;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert approved successfully" });
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/reject")]
        public async Task<ActionResult<Advert>> Reject(int id)
        {
            var advert = _context.Adverts.Find(id);
            advert.Status = Enums.AdvertStatus.Rejected;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert rejected successfully" });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var advertModel = await _context.Adverts.FindAsync(id);
            if (advertModel == null)
            {
                return NotFound();
            }

            _context.Adverts.Remove(advertModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Advert deleted successfully" });
        }
    }
}
