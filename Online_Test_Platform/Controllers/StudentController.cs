using Microsoft.AspNetCore.Http;
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
            try
            {
               //markscategory(1);
                RandomQuestion(1);
                var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
                int? userID = HttpContext.Session.GetInt32("UserID");
                string currentdate = DateTime.Now.ToShortDateString();
                var result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 1).FirstOrDefault();
                if (result == null)
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

        [HttpPost]
        public IActionResult UserData(IFormCollection ButtonCheck)
        {
            try
            {
                string UserAnswer = ButtonCheck["Question"].ToString();
                HttpContext.Session.SetString("UserAnswer", UserAnswer);
                UserMarksCal();
                return RedirectToAction("UserData");
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

        public IActionResult ReasoningQuestions()
        {
            try
            {
                QuestionCategory(2);
                int? userID = HttpContext.Session.GetInt32("UserID");
                string currentdate = DateTime.Now.ToShortDateString();
                var result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 2).FirstOrDefault();
                if (result == null)
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

        [HttpPost]
        public IActionResult ReasoningQuestions(IFormCollection ButtonCheck)
        {
            try
            {
                string UserAnswer = ButtonCheck["Question"].ToString();
                HttpContext.Session.SetString("UserAnswer", UserAnswer);
                UserMarksCal();
                return RedirectToAction("ReasoningQuestions");
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

        public IActionResult VerbalQuestions()
        {
            try
            {
                QuestionCategory(3);
                var data = HttpContext.Session.GetSessionData<Question>("CorrectAnswer");
                int? userID = HttpContext.Session.GetInt32("UserID");
                string currentdate = DateTime.Now.ToShortDateString();
                var result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestDate == currentdate && x.TestCatagoryId == 3).FirstOrDefault();
                if (result == null)
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

        [HttpPost]
        public IActionResult VerbalQuestions(IFormCollection ButtonCheck)
        {
            try
            {
                string UserAnswer = ButtonCheck["Question"].ToString();
                HttpContext.Session.SetString("UserAnswer", UserAnswer);
                UserMarksCal();
                return RedirectToAction("VerbalQuestions");
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

        public IActionResult UserResponse()
        {
            try
            {
                ExamEnd(1);
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

        [HttpPost]
        public IActionResult UserResponse(string a)
        {
            try
            {
                return RedirectToAction("Index", "Student");
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

        public IActionResult UserMarks()
        {
            try
            {
                HttpContext.Session.SetInt32("id", 1);
                int? userID = HttpContext.Session.GetInt32("UserID");
                var Result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 1).ToList();
                return View(Result);
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

        public IActionResult UserResponseReasoning()
        {
            try
            {
                ExamEnd(2);
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

        [HttpPost]
        public IActionResult UserResponseReasoning(string a)
        {
            try
            {
                return RedirectToAction("Index", "Student");
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

        public IActionResult UserResponseVerbal()
        {
            try
            {
                ExamEnd(3);
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

        [HttpPost]
        public IActionResult UserResponseVerbal(string a)
        {
            try
            {
                return RedirectToAction("Index", "Student");
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

        public IActionResult ReasoningMarks()
        {
            try
            {
                HttpContext.Session.SetInt32("idrea", 2);
                int? userID = HttpContext.Session.GetInt32("UserID");
                var Result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 2).ToList();
                return View(Result);
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

        public IActionResult VerbalMarks()
        {
            try
            {
                HttpContext.Session.SetInt32("idver", 3);
                int? userID = HttpContext.Session.GetInt32("UserID");
                var Result = testreport.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == 3).ToList();
                return View(Result);
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


        public void QuestionCategory(int categorynum)
        {

            try
            {
                Question q = new Question();
                int? questionid = HttpContext.Session.GetInt32("Qid");
                // int? m = questionid;
                if (questionid == null)
                {
                    var result = service.GetAsync().Result.Where(x => x.TestCatagoryId == categorynum).FirstOrDefault();
                    HttpContext.Session.SetSessionData<Question>("CorrectAnswer", result);
                    HttpContext.Session.SetInt32("Qid", result.QuestionId);

                }
                else
                {
                    questionid++;
                    var result = service.GetAsync().Result.Where(x => x.QuestionId == questionid && x.TestCatagoryId == categorynum).FirstOrDefault();
                    HttpContext.Session.SetSessionData<Question?>("CorrectAnswer", result);
                    if (result != null)
                    {
                        HttpContext.Session.SetInt32("Qid", result.QuestionId);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UserMarksCal()
        {
            try
            {
                string? useranswer = HttpContext.Session.GetString("UserAnswer");
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
                user.TestDate = DateTime.Now.ToShortDateString();
                user.Marks = (int)UserMarks;

                var result = answer.CreateAsync(user).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExamEnd(int catnum)
        {
            try
            {
                TestReport test = new TestReport();
                int? userID = HttpContext.Session.GetInt32("UserID");
                string? UserName = HttpContext.Session.GetString("UserName");
                var result = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == catnum && x.TestDate== DateTime.Now.ToShortDateString()).ToList();
                var NewResult = answer.GetAsync().Result.Where(x => x.UserId == userID && x.TestCatagoryId == catnum).FirstOrDefault();
                var marks = result.Sum(x => x.Marks);
                test.Marks = marks;
                test.UserId = userID;
                test.TestCatagoryId = NewResult?.TestCatagoryId;
                test.TestDate = DateTime.Now.ToShortDateString();
                test.TotalMarks = 10;
                test.UserName = UserName;
                var TestResult = testreport.CreateAsync(test);
                HttpContext.Session.Remove("Qid");
                
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                int num = 0;
                do
                {
                    num = Randomnumber();
                    if (!randomList.Contains(num))
                    {
                        var result = service.GetAsync().Result.Where(x => x.TestCatagoryId == categorynum && x.QuestionId == num).FirstOrDefault();
                        HttpContext.Session.SetSessionData<Question?>("CorrectAnswer", result);
                    }
                } while (randomList.Contains(num));

                if (!randomList.Contains(num))
                {
                    randomList.Add(num);
                }
              
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}







