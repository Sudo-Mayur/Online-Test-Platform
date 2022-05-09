using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class Question
    {
        public Question()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int QuestionId { get; set; }
        public int? TestCatagoryId { get; set; }
        public string? Question1 { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? CorrectAnswer { get; set; }

        public virtual TestCatagory? TestCatagory { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
