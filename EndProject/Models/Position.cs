using EndProject.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Models
{
    public class Position:BaseNameEntity
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}
