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
            var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 1);
            return View(res);
        }

        public IActionResult ReasoningTestReport()
        {
            var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 2);
            return View(res);
        }

        public IActionResult VerbalTestReport()
        {
            var res = service.GetAsync().Result.Where(x => x.TestCatagoryId == 2);
            return View(res);
        }

        public IActionResult Search(string SearchBy, int search)
        {

            if (search == 0)
            {
                ViewBag.Message = "No Record Found";
                var res = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(res);
            }
           else if (SearchBy == "UserID")
            {
                var res = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId==1).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }
               
                return View(res);
                               
            }
            else if (SearchBy == "Marks")
            {
                var res = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 1).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }
               
              return View(res);
                
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
                var res = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(res);
            }
            else if (SearchBy == "UserID")
            {
                var res = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId == 2).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }

                return View(res);

            }
            else if (SearchBy == "Marks")
            {
                var res = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 2).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }

                return View(res);

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
                var res = service.GetAsync().Result.Where(e => e.UserId == search).ToList();
                return View(res);
            }
            else if (SearchBy == "UserID")
            {
                var res = service.GetAsync().Result.Where(e => e.UserId == search && e.TestCatagoryId == 3).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                    // return RedirectToAction("Index");
                }

                return View(res);

            }
            else if (SearchBy == "Marks")
            {
                var res = service.GetAsync().Result.Where(e => e.Marks == search && e.TestCatagoryId == 3).ToList();
                if (res.Count == 0)
                {
                    ViewBag.Message = "No Record Found";

                }

                return View(res);

            }
            else
            {
                return View();
            }
        }
    }
}
