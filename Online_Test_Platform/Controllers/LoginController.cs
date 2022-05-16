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
        public async Task<IActionResult> Index(UserInfo user)
        {
            var res = service.GetAsync().Result.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
            if(res == null)
            {
                ViewBag.Message = "Wrong Credential";
                return View(user);

            }
            if(user.EmailId==res.EmailId)
            {
                var decryptedPassword = await DecryptAsync(res.UserPassword);
                if (user.UserPassword== decryptedPassword)
                {
                    HttpContext.Session.SetInt32("UserID", res.UserId);
                    if (res.RoleId==1)
                    {
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

        public async Task<string> DecryptAsync(string text)
        {
            var textToDecrypt = text;
            string toReturn = "";
            string publickey = "12345678";
            string secretkey = "87654321";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                toReturn = encoding.GetString(ms.ToArray());
            }
            return toReturn;
        }

    }
}




//P@ssw0rd