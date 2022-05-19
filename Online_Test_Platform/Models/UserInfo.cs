using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Email is Required")]
        [Email(ErrorMessage = "Please Enter  Email in Correct Format")]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [Password(ErrorMessage = "Please Enter Strong Password")]
        public string? UserPassword { get; set; }
        public int? RoleId { get; set; }
        public string? UserName { get; set; }

        public virtual UserRole? Role { get; set; }
        public virtual ICollection<TestReport> TestReports { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
