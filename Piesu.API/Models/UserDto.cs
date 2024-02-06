using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Piesu.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piesu.API.Models
{
    public class UserDto : IdentityUser
    {
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageURL { get; set; }
        public string[] Roles { get; set; }
    }
}
