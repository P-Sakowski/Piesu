using Piesu.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Breed
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Attitude AnimalsAttitude { get; set; }
        public Attitude PeopleAttitude { get; set; }
        public bool ConsideredAggressive { get; set; }
    }
}
