using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class TestReport
    {
        public int TestId { get; set; }
        public int? UserId { get; set; }
        public int? TestCatagoryId { get; set; }
        public int? Marks { get; set; }
        public string? TestDate { get; set; }
        public int? TotalMarks { get; set; }

        public virtual TestCatagory? TestCatagory { get; set; }
        public virtual UserInfo? User { get; set; }
    }
}
