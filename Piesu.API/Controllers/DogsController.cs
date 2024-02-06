using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Helpers;
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
    public class DogsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<AuthController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;
        public DogsController(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthController> logger,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IMapper mapper,
            IConfiguration configuration)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._httpContext = httpContextAccessor;
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public List<DogDto> Get()
        {
            var userEmail = GetEmailFromJWT();
            var user = _userManager.FindByEmailAsync(userEmail).Result;
            var dbDogs = _dbContext.Dogs.Where(d => d.OwnerId == user.Id).ToList();
            var dogs = new List<DogDto>();
            foreach (var dog in dbDogs)
            {
                dog.Breed = _dbContext.Breeds.Where(b => b.Id == dog.BreedId).FirstOrDefault();
                var model = _mapper.Map<DogDto>(dog);
                dogs.Add(model);
            }
            return dogs;
        }

        [Authorize]
        [HttpGet("{id}")]
        public DogDto Get(int id)
        {
            var dbDog = _dbContext.Dogs.Where(d => d.Id == id).FirstOrDefault();
            dbDog.Breed = _dbContext.Breeds.Where(b => b.Id == dbDog.BreedId).FirstOrDefault();
            var dog = _mapper.Map<DogDto>(dbDog);
            return dog;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DogEntity input)
        {
            if (!ModelState.IsValid || input == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid input format." });
            }
            var userEmail = GetEmailFromJWT();
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid user." });
            }
            input.UserId = user.Id;
            var dog = _mapper.Map<Dog>(input);
            _dbContext.Dogs.Add(dog);
            _dbContext.SaveChanges();

            return Ok(new { message = $"Dog {input.Name} added successfully" });
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DogEntity input)
        {
            if (!ModelState.IsValid || input == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid input format." });
            }
            var dog = _dbContext.Dogs.Find(id);
            dog.Name = input.Name;
            dog.BirthDate = input.BirthDate;
            dog.Breed = _dbContext.Breeds.Find(input.BreedId);

            _dbContext.SaveChanges();

            return Ok(new { message = $"Dog {input.Name} updated successfully" });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dog = _dbContext.Dogs.Find(id);
            _dbContext.Dogs.Remove(dog);
            _dbContext.SaveChanges();

            return Ok(new { message = $"Dog {dog.Name} deleted successfully" });
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile image)
        {
            var userEmail = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid user." });
            }
            var dog = _dbContext.Dogs.Find(id);
            var fileHelper = new FileHelper(_configuration);
            var img = new Image
            {
                Dog = dog,
                ImageURL = await fileHelper.UploadImageAsync(image),
                CreateDate = DateTime.Now
            };
            _dbContext.Images.Add(img);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Profile image uploaded succesfully." });
        }

        private string GetEmailFromJWT()
        {
            return _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
