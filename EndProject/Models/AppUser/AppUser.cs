using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Models.AppUser
{
    public class AppUser:IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Max 20 min 2 element ola bilər")]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Max 25 min 5 element ola bilər")]
        public string  LastName { get; set; }
    }
}
