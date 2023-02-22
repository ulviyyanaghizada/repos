using System.ComponentModel.DataAnnotations;

namespace EndProject.Models.ViewModels 
{ 
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
