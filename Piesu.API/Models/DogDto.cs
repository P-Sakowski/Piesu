using Piesu.API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class DogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public Breed Breed { get; set; }
        public ICollection<Image> Photos { get; set; }
    }
}
