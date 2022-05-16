using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;
using Online_Test_Platform.SessionExtension;
using System.Collections;

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
            //markscategory(1);
            RandomQuestion(1);
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            int? userID = HttpContext.Session.GetInt32("UserID");
            string currentdate = DateTime.Now.ToShortDateString();
            var res = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 1).FirstOrDefault();
            if (res == null)
            {
                if (randomList.Count == 10)
                {
                    return RedirectToAction("UserResponse");
                }
                return View(data);
            }
            else
            {
                return View("RepeatExam");
            }

        }

        [HttpPost]
        public IActionResult UserData(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            HttpContext.Session.SetString("UserAnswer", uanswer);
            UserMarksCal();
            return RedirectToAction("UserData");
        }


        public IActionResult ReasoningQuestions()
        {
            markscategory(2);
            int? userID = HttpContext.Session.GetInt32("UserID");
            string currentdate = DateTime.Now.ToShortDateString();
            var res = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 2).FirstOrDefault();
            if (res == null)
            {
                var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
                if (data == null)
                {
                    return RedirectToAction("UserResponseReasoning");
                }
                return View(data);
            }
            else
            {
                return View("RepeatExam");
            }

        }

        [HttpPost]
        public IActionResult ReasoningQuestions(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            HttpContext.Session.SetString("UserAnswer", uanswer);
            UserMarksCal();
            return RedirectToAction("ReasoningQuestions");

        }

        public IActionResult VerbalQuestions()
        {
            markscategory(3);
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            int? userID = HttpContext.Session.GetInt32("UserID");
            string currentdate = DateTime.Now.ToShortDateString();
            var res = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 3).FirstOrDefault();
            if (res == null)
            {
                if (data == null)
                {
                    return RedirectToAction("UserResponseVerbal");
                }
                return View(data);
            }
            else
            {
                return View("RepeatExam");
            }

        }

        [HttpPost]
        public IActionResult VerbalQuestions(IFormCollection frm)
        {
            string uanswer = frm["Mayur"].ToString();
            HttpContext.Session.SetString("UserAnswer", uanswer);
            UserMarksCal();
            return RedirectToAction("VerbalQuestions");

        }

        public IActionResult UserResponse()
        {
            ExamEnd(1);
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
            ExamEnd(2);
            return View();
        }

        [HttpPost]
        public IActionResult UserResponseReasoning(string a)
        {
            return RedirectToAction("Index", "Student");
        }

        public IActionResult UserResponseVerbal()
        {
            ExamEnd(3);
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


        public void markscategory(int categorynum)
        {

            Question q = new Question();
            int? questionid = HttpContext.Session.GetInt32("id");
            int? m = questionid;
            if (m == null)
            {
                var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == categorynum).FirstOrDefault();
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                HttpContext.Session.SetInt32("id", res.QuestionId);

            }
            else
            {

                int? id1 = HttpContext.Session.GetInt32("id");
                id1++;
                var res = service.GetAsync().Result.Where(x => x.QuestionId == id1 && x.TestCatagoryId == categorynum).FirstOrDefault();
                HttpContext.Session.SetSessionData<Question>("CorrectAnswer", res);
                if (res != null)
                {
                    HttpContext.Session.SetInt32("id", res.QuestionId);
                }

            }
        }

        public void UserMarksCal()
        {
            string useranswer = HttpContext.Session.GetString("UserAnswer");
            var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
            UserAnswer user = new UserAnswer();


            if (useranswer != null)
            {

                if (useranswer == data.CorrectAnswer)
                {
                    user.Marks = 1;
                    HttpContext.Session.SetInt32("UserMarks", user.Marks);

                }
                else
                {
                    user.Marks = 0;
                    HttpContext.Session.SetInt32("UserMarks", user.Marks);

                }

            }
            else
            {
                user.Marks = 0;
                HttpContext.Session.SetInt32("UserMarks", user.Marks);

            }


            int? userID = HttpContext.Session.GetInt32("UserID");
            int? UserMarks = HttpContext.Session.GetInt32("UserMarks");

            user.QuestionId = data.QuestionId;
            user.UserId = userID;
            user.TestCatagoryId = data.TestCatagoryId;
            user.UserAnswer1 = useranswer;
            user.Marks = (int)UserMarks;

            var res = answer.CreateAsync(user).Result;
        }

        public void ExamEnd(int catnum)
        {
            TestReport test = new TestReport();
            int? userID = HttpContext.Session.GetInt32("UserID");
            var res = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == catnum).ToList();
            var res1 = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == catnum).FirstOrDefault();
            var marks = res.Sum(x => x.Marks);
            test.Marks = marks;
            test.UserId = userID;
            test.TestCatagoryId = res1.TestCatagoryId;
            test.TestDate = DateTime.Now.ToShortDateString();
            if (catnum == 1)
            {
                test.TotalMarks = 10;
            }
            else if (catnum == 2)
            {
                test.TotalMarks = 10;
            }
            else
            {
                test.TotalMarks = 10;
            }

            var res2 = testreport.CreateAsync(test);
        }

        public IActionResult RepeatExam()
        {
            return View();
        }

        public int Randomnumber()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 11);
            return num;
        }


        public static List<int> randomList = new List<int>();
        public void RandomQuestion(int categorynum)
        {
            int num = 0;
            do
            {
                num = Randomnumber();
                if (!randomList.Contains(num))
                {
                    var resnew = service.GetAsync().Result.Where(x => x.TestCatagoryId == categorynum && x.QuestionId == num).FirstOrDefault();
                    HttpContext.Session.SetSessionData<Question>("CorrectAnswer", resnew);
                }
            } while (randomList.Contains(num));

            if (!randomList.Contains(num))
            {
                randomList.Add(num);
            }
        }

    }
}







