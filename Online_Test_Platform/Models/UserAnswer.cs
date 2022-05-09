using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class UserAnswer
    {
        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public int? UserId { get; set; }
        public int? TestCatagoryId { get; set; }
        public string? UserAnswer1 { get; set; }
        public int Marks { get; set; }

        public virtual Question? Question { get; set; }
        public virtual TestCatagory? TestCatagory { get; set; }
        public virtual UserInfo? User { get; set; }
    }
}
