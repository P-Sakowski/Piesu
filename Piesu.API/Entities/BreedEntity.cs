using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Entities
{
    public class BreedEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public short AnimalsAttitude { get; set; }
        public short PeopleAttitude { get; set; }
        public bool ConsideredAggressive { get; set; }
    }
}
