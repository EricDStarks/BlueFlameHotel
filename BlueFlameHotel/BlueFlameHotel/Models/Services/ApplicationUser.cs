using BlueFlameHotel.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlueFlameHotel.Models.Services
{
    public class ApplicationUser: IdentityUser
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        public string? Token { get; set; }

        [NotMapped]
        public IList<string>? Roles { get; set; }
    }
}
