using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;

namespace Online_Test_Platform.Controllers
{
    public class DetailMarksController : Controller
    {
        private readonly IService<Question, int> service;
        private readonly IService<UserAnswer, int> Useranswer;

        public DetailMarksController(IService<Question, int> service, IService<UserAnswer, int> useranswer)
        {
            this.service = service;
            Useranswer = useranswer;
        }

        public IActionResult MarksDetails()
        {
            try
            {
                int? questionidAp = HttpContext.Session.GetInt32("id");
                CategoryID(questionidAp);
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
            int? questionidrea = HttpContext.Session.GetInt32("idrea");
            CategoryID(questionidrea);
            return View();
        }
        public IActionResult MarksDetailsVerbal()
        {
            int? questionidver = HttpContext.Session.GetInt32("idver");
            CategoryID(questionidver);
            return View();
        }

        public IActionResult CategoryID(int? CatID)
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var resQues = service.GetAsync().Result.Where(x => x.TestCatagoryId == CatID).ToList();
            var resUser = Useranswer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == CatID).ToList();

            var Resultant = from e in resQues
                            join d in resUser on
                            e.QuestionId equals d.QuestionId
                            select new
                            {
                                QuestionID = e.QuestionId,
                                Question = e.Question1,
                                CorrectAns = e.CorrectAnswer,
                                UserAns = d.UserAnswer1,
                                Marks = d.Marks,
                            };

            List<MarksDetails> marksdetails = new List<MarksDetails>();
            foreach (var d in Resultant)
            {
                marksdetails.Add(new MarksDetails() { QuestionID = d.QuestionID, Question = d.Question, CorrectAns = d.CorrectAns, UserAns = d.UserAns, Marks = d.Marks });
            }

            return View(marksdetails);
        }
    }
}
