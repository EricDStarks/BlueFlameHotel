using BlueFlameHotel.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueFlameHotel.Models.Services
{
    public class ApplicationUser: IdentityUser
    {
        public string Password { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        public string? Token { get; set; }

        [NotMapped]
        public IList<string>? Roles { get; set; }
    }
}
