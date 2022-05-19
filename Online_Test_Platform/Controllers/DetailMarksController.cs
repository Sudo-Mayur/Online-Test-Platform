using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;

namespace Online_Test_Platform.Controllers
{
    public class DetailMarksController : Controller
    {
        private readonly IService<Question, int> service;
        private readonly IService<UserAnswer, int> useranswer;

        public DetailMarksController(IService<Question, int> service, IService<UserAnswer, int> useranswer)
        {
            this.service = service;
            this.useranswer = useranswer;
        }

        public IActionResult MarksDetails()
        {
            try
            {
            
                int? AptitudeQuestion = HttpContext.Session.GetInt32("id");
                CategoryID(AptitudeQuestion);
                //throw new Exception();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = RouteData?.Values?["controller"]?.ToString(),
                    ActionName = RouteData?.Values?["action"]?.ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }
        public IActionResult MarksDetailsReasoning()
        {

            try
            {
                int? ReasoningQuestion = HttpContext.Session.GetInt32("idrea");
                CategoryID(ReasoningQuestion);
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = RouteData?.Values?["controller"]?.ToString(),
                    ActionName = RouteData?.Values?["action"]?.ToString(),
                    ErrorMessage = ex.Message
                });

            }
        }
        public IActionResult MarksDetailsVerbal()
        {
            try
            {
                int? VerbalQuestion = HttpContext.Session.GetInt32("idver");
                CategoryID(VerbalQuestion);
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = RouteData?.Values?["controller"]?.ToString(),
                    ActionName = RouteData?.Values?["action"]?.ToString(),
                    ErrorMessage = ex.Message
                });

            }
        }

        public IActionResult CategoryID(int? CatID)
        {
            try
            {
                int? userID = HttpContext.Session.GetInt32("UserID");
                var QuestionResult = service.GetAsync().Result.Where(x => x.TestCatagoryId == CatID).ToList();
                var UserResult = useranswer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == CatID).ToList();

                var Resultant = from Question in QuestionResult
                                join Answer in UserResult on
                                Question.QuestionId equals Answer.QuestionId
                                select new
                                {
                                    QuestionID = Question.QuestionId,
                                    Question = Question.Question1,
                                    CorrectAns = Question.CorrectAnswer,
                                    UserAns = Answer.UserAnswer1,
                                    Marks = Answer.Marks,
                                };

                List<MarksDetails> marksdetails = new List<MarksDetails>();
                foreach (var item in Resultant)
                {
                    marksdetails.Add(new MarksDetails() { QuestionID = item.QuestionID, Question = item.Question, CorrectAns = item.CorrectAns, UserAns = item.UserAns, Marks = item.Marks });
                }

                return View(marksdetails);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = RouteData?.Values?["controller"]?.ToString(),
                    ActionName = RouteData?.Values?["action"]?.ToString(),
                    ErrorMessage = ex.Message
                });

            }
        }
    }
}
