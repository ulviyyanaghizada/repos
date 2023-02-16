using EndProject.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Models
{
    public class Setting : BaseEntity
    {
        [Required]
        public string Key { get; set; }
        public string? Value { get; set; }
    }
}
