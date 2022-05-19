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
                        HttpContext.Session.SetString("UserName", res.UserName);
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

        public async Task<IActionResult> Create()
        {
            var user = new UserInfo();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserInfo Info)
        {
           var UserData= service.GetAsync().Result.Where(x=>x.EmailId==Info.EmailId).FirstOrDefault();
            if(UserData==null)
            {
                if (Info.UserPassword == null || Info.EmailId == null || Info.UserName==null)
                {
                    ViewBag.Message = "Please Enter Email Password and Name";
                    return View(Info);
                }

                if (ModelState.IsValid)
                {
                    Info.RoleId = 1;
                    Info.UserPassword = EncryptAsync(Info.UserPassword);
                    var res = await service.CreateAsync(Info);
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

        public string EncryptAsync(string message)
        {
            var textToEncrypt = message;
            string toReturn = string.Empty;
            string publicKey = "12345678";
            string secretKey = "87654321";
            byte[] secretkeyByte;
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
            byte[] publickeybyte;
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                toReturn = Convert.ToBase64String(ms.ToArray());
            }
            return toReturn;

        }

    }
}




//P@ssw0rd