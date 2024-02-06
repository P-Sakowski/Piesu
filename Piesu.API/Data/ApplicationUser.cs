using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Piesu.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piesu.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageURL { get; set; }
    }
}
