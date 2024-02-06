using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Piesu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;
        public BreedsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public List<Breed> Get()
        {
            var dbBreeds = _dbContext.Breeds.ToList();
            var breeds = new List<Breed>();
            foreach (var breed in dbBreeds)
            {
                var model = _mapper.Map<Breed>(breed);
                breeds.Add(model);
            }
            return breeds;
        }

        [HttpGet("{id}")]
        public Breed Get(int id)
        {
            var dbBreed = _dbContext.Breeds.Where(b => b.Id == id).FirstOrDefault();
            var breed = _mapper.Map<Breed>(dbBreed);
            return breed;
        }

        [HttpPost]
        public IActionResult Post([FromBody] BreedEntity input)
        {
            if (!ModelState.IsValid || input == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid input format." });
            }
            var breed = _mapper.Map<Breed>(input);
            _dbContext.Breeds.Add(breed);
            _dbContext.SaveChanges();

            return Ok(new { message = $"Breed {input.Name} added successfully" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BreedEntity input)
        {
            if (!ModelState.IsValid || input == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid input format." });
            }
            var breed = _dbContext.Breeds.Find(id);
            breed.Name = input.Name;
            breed.ConsideredAggressive = input.ConsideredAggressive;
            breed.AnimalsAttitude = (Enums.Attitude)input.AnimalsAttitude;
            breed.PeopleAttitude = (Enums.Attitude)input.PeopleAttitude;
            _dbContext.SaveChanges();

            return Ok(new { message = $"Breed {input.Name} updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var breed = _dbContext.Breeds.Find(id);
            _dbContext.Breeds.Remove(breed);
            _dbContext.SaveChanges();

            return Ok(new { message = $"Breed {breed.Name} deleted successfully" });
        }
    }
}
