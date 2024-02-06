using Piesu.API.Data;
using Piesu.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        public Location Location { get; set; }
        public DateTime CreateDate { get; set; }
        public ApplicationUser Author { get; set; }
        public CategoryDto Category { get; set; }
        public SubcategoryDto Subcategory { get; set; }
        public AdvertStatus Status { get; set; }
        public string MainPhotoURL { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
        public virtual ICollection<Image> Photos { get; set; }
    }
}