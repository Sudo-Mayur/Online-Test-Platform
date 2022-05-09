using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;
using Online_Test_Platform.SessionExtension;


namespace Online_Test_Platform.Controllers
{
    public class StudentController : Controller
    {
        private readonly IService<Question, int> service;
        private readonly IService<TestReport, int> report;
        private readonly IService<UserAnswer, int> answer;
        private readonly IService<TestReport, int> testreport;
        public StudentController(IService<Question, int> service, IService<TestReport, int> report, IService<UserAnswer, int> answer, IService<TestReport, int> testreport)
        {
            this.service = service;
            this.report = report;
            this.answer = answer;
            this.testreport = testreport;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestCategory()
        {
            

            return View();
        }

        public IActionResult CategoryMarks()
        {
            return View();
        }
        public IActionResult UserData()
        {
            Question q = new Question();
            int? questionid = HttpContext.Session.GetInt32("id");
            int? m = questionid;
            if (m == null)
            {
                var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 1).FirstOrDefault();
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);
                return View(res);

            }
            else
            {

                int? id1 = HttpContext.Session.GetInt32("id");
                id1++;
                var res = service.GetAsync().Result.Where(x => x.QuestionId == id1 && x.TestCatagoryId == 1).FirstOrDefault();
                if (res == null)
                {
                    return RedirectToAction("UserResponse");
                }
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);

                return View(res);

            }

        }

        [HttpPost]
        public IActionResult UserData(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            UserAnswer user = new UserAnswer();


            if (uanswer != null)
            {

                if (uanswer == data.CorrectAnswer)
                {
                    user.Marks = 1;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }
                else
                {
                    user.Marks = 0;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }

            }
            else
            {
                user.Marks = 0;
                HttpContext.Session.SetInt32("useranswer", user.Marks);

            }


            int? userID = HttpContext.Session.GetInt32("UserID");
            int? Useranswer = HttpContext.Session.GetInt32("useranswer");

            user.QuestionId = data.QuestionId;
            user.UserId = userID;
            user.TestCatagoryId = data.TestCatagoryId;
            user.UserAnswer1 = uanswer;
            user.Marks = (int)Useranswer;

            var res = answer.CreateAsync(user).Result;
            return RedirectToAction("UserData");
        }


        public IActionResult ReasoningQuestions()
        {
            Question q = new Question();
            int? questionid = HttpContext.Session.GetInt32("id");
            int? m = questionid;
            if (m == null)
            {
                var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 2).FirstOrDefault();
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);
                return View(res);

            }
            else
            {

                int? id1 = HttpContext.Session.GetInt32("id");
                id1++;
                var res = service.GetAsync().Result.Where(x => x.QuestionId == id1 && x.TestCatagoryId == 2).FirstOrDefault();
                if (res == null)
                {
                    return RedirectToAction("UserResponseReasoning");
                }
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);

                return View(res);

            }
        }

        [HttpPost]
        public IActionResult ReasoningQuestions(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            UserAnswer user = new UserAnswer();

            if (uanswer != null)
            {

                if (uanswer == data.CorrectAnswer)
                {
                    user.Marks = 1;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }
                else
                {
                    user.Marks = 0;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }

            }
            else
            {
                user.Marks = 0;
                HttpContext.Session.SetInt32("useranswer", user.Marks);

            }


            int? userID = HttpContext.Session.GetInt32("UserID");
            int? Useranswer = HttpContext.Session.GetInt32("useranswer");

            user.QuestionId = data.QuestionId;
            user.UserId = userID;
            user.TestCatagoryId = data.TestCatagoryId;
            user.UserAnswer1 = uanswer;
            user.Marks = (int)Useranswer;

            var res = answer.CreateAsync(user).Result;
            return RedirectToAction("ReasoningQuestions");
        }

        public IActionResult VerbalQuestions()
        {
            Question q = new Question();
            int? questionid = HttpContext.Session.GetInt32("id");
            int? m = questionid;
            if (m == null)
            {
                var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 3).FirstOrDefault();
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);
                return View(res);

            }
            else
            {

                int? id1 = HttpContext.Session.GetInt32("id");
                id1++;
                var res = service.GetAsync().Result.Where(x => x.QuestionId == id1 && x.TestCatagoryId == 3).FirstOrDefault();
                if (res == null)
                {
                    return RedirectToAction("UserResponseVerbal", "Student");
                }
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);

                return View(res);
            }
        }

        [HttpPost]
        public IActionResult VerbalQuestions(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            UserAnswer user = new UserAnswer();

            if (uanswer != null)
            {

                if (uanswer == data.CorrectAnswer)
                {
                    user.Marks = 1;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }
                else
                {
                    user.Marks = 0;
                    HttpContext.Session.SetInt32("useranswer", user.Marks);

                }

            }
            else
            {
                user.Marks = 0;
                HttpContext.Session.SetInt32("useranswer", user.Marks);

            }


            int? userID = HttpContext.Session.GetInt32("UserID");
            int? Useranswer = HttpContext.Session.GetInt32("useranswer");

            user.QuestionId = data.QuestionId;
            user.UserId = userID;
            user.TestCatagoryId = data.TestCatagoryId;
            user.UserAnswer1 = uanswer;
            user.Marks = (int)Useranswer;

            var res = answer.CreateAsync(user).Result;
            return RedirectToAction("VerbalQuestions");
        }

        public IActionResult UserResponse()
        {
            TestReport test = new TestReport();
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 1).ToList();
            var res1 = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 1).FirstOrDefault();
            var marks = res.Sum(x => x.Marks);
            test.Marks = marks;
            test.UserId = userID;
            test.TestCatagoryId = res1.TestCatagoryId;
            test.TestDate = DateTime.Now.ToShortDateString();
            test.TotalMarks = 4;
            var res2 = testreport.CreateAsync(test);
            return View();
        }

        [HttpPost]
        public IActionResult UserResponse(string a)
        {
            return RedirectToAction("Index", "Student");
        }

        public IActionResult UserMarks()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res3 = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 1).ToList();
            return View(res3);
        }

        public IActionResult UserResponseReasoning()
        {
            TestReport test = new TestReport();
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 2).ToList();
            var res1 = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 2).FirstOrDefault();
            var marks = res.Sum(x => x.Marks);
            test.Marks = marks;
            test.UserId = userID;
            test.TestCatagoryId = res1.TestCatagoryId;
            test.TestDate = DateTime.Now.ToShortDateString();
            test.TotalMarks = 2;
            var res2 = testreport.CreateAsync(test);
            return View();

        }

        [HttpPost]
        public IActionResult UserResponseReasoning(string a)
        {
            return RedirectToAction("Index", "Student");
        }

        public IActionResult UserResponseVerbal()
        {
            TestReport test = new TestReport();
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 3).ToList();
            var res1 = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 3).FirstOrDefault();
            var marks = res.Sum(x => x.Marks);
            test.Marks = marks;
            test.UserId = userID;
            test.TestCatagoryId = res1.TestCatagoryId;
            test.TestDate = DateTime.Now.ToShortDateString();
            test.TotalMarks = 2;
            var res2 = testreport.CreateAsync(test);
            return View();

        }

        [HttpPost]
        public IActionResult UserResponseVerbal(string a)
        {
            return RedirectToAction("Index", "Student");
        }

        public IActionResult ReasoningMarks()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res3 = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 2).ToList();
            return View(res3);
        }

        public IActionResult VerbalMarks()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res3 = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 3).ToList();
            return View(res3);
        }


        public IActionResult AptitudeTest()
        {
            List<Question> li = service.GetAsync().Result.Where(x => x.TestCatagoryId == 1).ToList();
            Queue<Question> queue = new Queue<Question>();
            foreach (Question a in li)
            {
                queue.Enqueue(a);
            }
          
            TempData["questions"] = queue;
            TempData.Keep();


            Question q = null;
            if (TempData["questions"] != null)
            {
                Queue<Question> qlist = (Queue<Question>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
                    TempData.Keep();
                }
            }
            return View(q);

        }

        [HttpPost]
        public IActionResult AptitudeTest(IFormCollection frm)
        {
            return RedirectToAction("AptitudeTest");
        }
    }
}



