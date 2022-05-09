using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            TestReports = new HashSet<TestReport>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int UserId { get; set; }
        public string? EmailId { get; set; }
        public string? UserPassword { get; set; }
        public int? RoleId { get; set; }

        public virtual UserRole? Role { get; set; }
        public virtual ICollection<TestReport> TestReports { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
