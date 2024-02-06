﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piesu.API.Models
{
    public class SubcategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
