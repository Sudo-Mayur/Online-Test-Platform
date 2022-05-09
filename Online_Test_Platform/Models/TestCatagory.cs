using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class TestCatagory
    {
        public TestCatagory()
        {
            Questions = new HashSet<Question>();
            TestReports = new HashSet<TestReport>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int TestCatagoryId { get; set; }
        public string? TestType { get; set; }
        public string? TestDuration { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestReport> TestReports { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
