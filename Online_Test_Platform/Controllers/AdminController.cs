using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;

namespace Online_Test_Platform.Controllers
{
    public class AdminController : Controller
    {
        private readonly IService<TestReport, int> service;

        public AdminController(IService<TestReport, int> service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AptitudeTestReport()
        {
            var Result = service.GetAsync().Result.Where(x => x.TestCatagoryId == 1);
            return View(Result);
        }

        public IActionResult ReasoningTestReport()
        {
            var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 2);
            return View(res);
        }

        public IActionResult VerbalTestReport()
        {
            var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 3);
            return View(res);
        }

        public IActionResult Search(string SearchBy, int search)
        {
            if (search == 0)
            {
                ViewBag.Message = "No Record Found";
                var Result = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(Result);
            }
           else if (SearchBy == "UserID")
            {
                var Result = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId==1).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }
               
                return View(Result);
                               
            }
            else if (SearchBy == "Marks")
            {
                var Result = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 1).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }
               
              return View(Result);
                
            }
            else
            {
                return View();
            }
        }

        public IActionResult ReasoningSearch(string SearchBy, int search)
        {

            if (search == 0)
            {
                ViewBag.Message = "No Record Found";
                var Result = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(Result);
            }
            else if (SearchBy == "UserID")
            {
                var Result = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId == 2).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }

                return View(Result);

            }
            else if (SearchBy == "Marks")
            {
                var Result = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 2).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }

                return View(Result);

            }
            else
            {
                return View();
            }
        }

        public IActionResult VerbalSearch(string SearchBy, int search)
        {

            if (search == 0)
            {
                ViewBag.Message = "No Record Found";
                var Result = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(Result);
            }
            else if (SearchBy == "UserID")
            {
                var Result = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId == 3).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }

                return View(Result);

            }
            else if (SearchBy == "Marks")
            {
                var Result = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 3).ToList();
                if (Result.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }

                return View(Result);

            }
            else
            {
                return View();
            }
        }
    }
}

