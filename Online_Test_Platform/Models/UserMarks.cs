namespace Online_Test_Platform.Models
{
    public class UserMarks
    {
        public int TestId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? TestCatagoryId { get; set; }
        public string TestCategory { get; set; }
        public int? Marks { get; set; }
        public string? TestDate { get; set; }
        public int? TotalMarks { get; set; }

    }
}
