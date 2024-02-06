using System;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public Dog Dog { get; set; }
        public Advert Advert { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}