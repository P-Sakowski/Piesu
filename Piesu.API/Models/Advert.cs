using Piesu.API.Data;
using Piesu.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Advert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsNegotiable { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
        public Category Category { get; set; }
        public virtual int CategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public virtual int SubcategoryId { get; set; }
        [Required]
        public AdvertStatus Status { get; set; }
        public string MainPhotoURL { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
        public virtual ICollection<Image> Photos { get; set; }
    }
}