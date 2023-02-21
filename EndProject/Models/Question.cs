using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Question:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
    }
}
