using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Advert> Adverts { get; set; }
    }
}
