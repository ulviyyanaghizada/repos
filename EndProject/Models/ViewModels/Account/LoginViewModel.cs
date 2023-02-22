using System.ComponentModel.DataAnnotations;

namespace EndProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string EmailOrUsername { get; set; }

        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
