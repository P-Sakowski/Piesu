using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        public string District { get; set; }
    }
}