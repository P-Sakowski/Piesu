using System;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Entities
{
    public class DogEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserId { get; set; }
        public int BreedId { get; set; }
        public string Description { get; set; }
    }
}
