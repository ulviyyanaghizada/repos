namespace EndProject.Models.ViewModels
{
    public class ContactVM
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string subject { get; set; }
        public string Message { get; set; }


    }
}
