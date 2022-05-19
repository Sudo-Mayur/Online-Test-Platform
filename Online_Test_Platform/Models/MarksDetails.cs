namespace Online_Test_Platform.Models
{
    public class MarksDetails
    {
        public int QuestionID { get; set; }
        public string? Question { get; set; }
        public string? CorrectAns { get; set; }
        public string? UserAns { get; set; }
        public int Marks { get; set; }
    }
}
