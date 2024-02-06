using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
