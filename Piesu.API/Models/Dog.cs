using Piesu.API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }
        public virtual string OwnerId { get; set; }
        public Breed Breed { get; set; }
        public virtual int BreedId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Advert> Adverts { get; set; }
        public virtual ICollection<Image> Photos { get; set; }
    }
}
