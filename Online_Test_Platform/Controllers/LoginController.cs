using Microsoft.AspNetCore.Mvc;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;
using System.Security.Cryptography;
using System.Text;

namespace Online_Test_Platform.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService<UserInfo, int> service;
        public LoginController(IService<UserInfo, int> service)
        {
            this.service = service;
        }
        
        public IActionResult Index()
        {         
            return View(new UserInfo());
        }

        [HttpPost]
        public IActionResult Index(UserInfo user)
        {
            try
            {
                var result = service.GetAsync().Result.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.Message = "Wrong Credential";
                    return View(user);

                }
                if (user.EmailId == result.EmailId)
                {
                    var decryptedPassword = DecryptAsync(result.UserPassword);
                    if (user.UserPassword == decryptedPassword)
                    {
                        HttpContext.Session.SetInt32("UserID", result.UserId);

                        if (result.RoleId == 1)
                        {
                            HttpContext.Session.SetString("UserName", result.UserName);
                            return RedirectToAction("Index", "Student");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Wrong Password";
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.Message = "Wrong EmailID";
                    return View(user);
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

        public IActionResult Create()
        {
            var user = new UserInfo();
            return View(user);
        }
        [HttpPost]
        public IActionResult Create(UserInfo Info)
        {
            try
            {
                var UserData = service.GetAsync().Result.Where(x => x.EmailId == Info.EmailId).FirstOrDefault();
                if (UserData == null)
                {
                    if (Info.UserPassword == null || Info.EmailId == null || Info.UserName == null)
                    {
                        ViewBag.Message = "Please Enter Email Password and Name";
                        return View(Info);
                    }

                    if (ModelState.IsValid)
                    {
                        Info.RoleId = 1;
                        Info.UserPassword = EncryptAsync(Info.UserPassword);
                        var res = service.CreateAsync(Info);
                        ViewBag.Message = "User Register Successfully";
                        return View(Info);
                    }
                    else
                    {
                        return View(Info);
                    }
                }
                else
                {
                    ViewBag.Message = "EmailID in Already Register,Please Enter Correct EmailID";
                    return View(Info);
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


        public string EncryptAsync(string message)
        {
            try
            {
                var textToEncrypt = message;
                string toReturn = string.Empty;
                string publicKey = "12345678";
                string secretKey = "87654321";
                byte[] secretkeyByte;
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                byte[] publickeybyte;
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publicKey);

                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DES des = DES.Create())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    toReturn = Convert.ToBase64String(ms.ToArray());
                }
                return toReturn;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public string DecryptAsync(string text)
        {
            try
            {
                var textToDecrypt = text;
                string toReturn = "";
                string publickey = "12345678";
                string secretkey = "87654321";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);

                byte[] inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DES des = DES.Create())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    toReturn = encoding.GetString(ms.ToArray());
                }
                return toReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}




