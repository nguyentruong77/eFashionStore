using eFashionStore.MailService;
using eFashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace eFashionStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly eFashionStoreDataContext _context;

        public AccountController()
        {
            _context = new eFashionStoreDataContext();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.NguoiDungs.FirstOrDefault(x => x.Email == model.Email);
                var checkUsername = _context.NguoiDungs.FirstOrDefault(x => x.TenTaiKhoan == model.TenTaiKhoan);
                var checkPhoneNumber = _context.NguoiDungs.FirstOrDefault(x => x.SDT == model.Sdt);

                if (checkEmail != null)
                {
                    ViewBag.EmailError = "Email already exists!";
                }
                if (checkUsername != null)
                {
                    ViewBag.UsernameError = "Username already exists!";
                }
                if (checkPhoneNumber != null)
                {
                    ViewBag.PhoneNumberError = "Phone number already exists!";
                }

                // Save user
                if (checkEmail == null && checkUsername == null && checkPhoneNumber == null)
                {
                    var user = new NguoiDung
                    {
                        HoTen = model.HoTen,
                        Email = model.Email,
                        DiaChi = model.DiaChi,
                        SDT = model.Sdt,
                        TenTaiKhoan = model.TenTaiKhoan,
                        MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau),
                        IsAdmin = false
                    };

                    _context.NguoiDungs.InsertOnSubmit(user);
                    _context.SubmitChanges();
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.MissingInformation = "Please fill in all the information!";
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _context.NguoiDungs.SingleOrDefault(u => u.TenTaiKhoan == model.TenTaiKhoan);

                if (user == null)
                {
                    ViewBag.UsernameError = "Username not registered!";
                }
                else if (!BCrypt.Net.BCrypt.Verify(model.MatKhau, user.MatKhau))
                {
                    ViewBag.PasswordError = "Incorrect password!";
                }
                else
                {
                    //save session
                    Session["UserName"] = user.TenTaiKhoan;
                    Session["UserId"] = user.UserID;

                    FormsAuthentication.SetAuthCookie(user.TenTaiKhoan, false);
                    
                    if ((bool)user.IsAdmin)
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin", UserName = user.TenTaiKhoan });
                    }    

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ViewBag.MissingInformation = "Please fill in all the information!";
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.FailedRecovery = "Please provide all the information!";
                return View();
            }

            var user = _context.NguoiDungs.SingleOrDefault(u => u.Email == Email);
            if (user == null)
            {
                ViewBag.FailedRecovery = "Email is not registered!";
                return View();
            }

            try
            {
                string randomCode = new Random().Next(100000, 999999).ToString();
                TempData["RandomCode"] = randomCode;
                TempData["UserId"] = user.UserID;

                var mailContent = new MailMessage
                {
                    From = new MailAddress("no-reply@yourdomain.com"),
                    Subject = $"Account Confirmation {user.TenTaiKhoan}",
                    Body = $@"
        <div style='font-family: Helvetica, Arial, sans-serif; min-width: 1000px; overflow: auto; line-height: 2'>
            <div style='margin: 50px auto; width: 70%; padding: 20px 0'>
                <div style='border-bottom: 1px solid #eee'>
                    <a href='' style='font-size: 1.4em; color: #00466a; text-decoration: none; font-weight: 600'>MobilePhone Store</a>
                </div>
                <p style='font-size: 1.1em'>Hello {user.HoTen},</p>
                <p>We have received a request to reset the password for your account. Enter the following password reset code:</p>
                <h2 style='background: #00466a; margin: 0 auto; width: max-content; padding: 0 10px; color: #fff; border-radius: 4px;'>{randomCode}</h2>
                <p style='font-size: 0.9em;'>Regards,<br />MobilePhone Store</p>
                <hr style='border: none; border-top: 1px solid #eee' />
                <div style='float: right; padding: 8px 0; color: #aaa; font-size: 0.8em; line-height: 1; font-weight: 300'>
                    <p>MobilePhone Store</p>
                    <p>97 Võ Văn Tần, District 3</p>
                    <p>TP HCM</p>
                </div>
            </div>
        </div>",
                    IsBodyHtml = true
                };

                mailContent.To.Add(user.Email);

                using (var smtpClient = new SmtpClient("smtp.yourmailserver.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(mailContent);
                }

                return RedirectToAction("ResetPassword");
            }
            catch (Exception ex)
            {
                // Log exception
                ViewBag.FailedRecovery = "An error occurred while sending the email. Please try again later.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string VerificationCode, string Password)
        {
            if (string.IsNullOrEmpty(VerificationCode) || string.IsNullOrEmpty(Password))
            {
                ViewBag.MissingInfo = "Please provide all the information!";
                return View();
            }

            string storedCode = TempData["RandomCode"] as string;
            int? userId = TempData["UserId"] as int?;

            if (userId == null)
            {
                ViewBag.MissingInfo = "Session has expired. Please try again.";
                return View();
            }

            var user = _context.NguoiDungs.SingleOrDefault(u => u.UserID == userId);
            if (user == null)
            {
                ViewBag.MissingInfo = "User does not exist.";
                return View();
            }

            if (VerificationCode == storedCode)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.MatKhau = BCrypt.Net.BCrypt.HashPassword(Password, salt);
                _context.SubmitChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.CodeError = "Incorrect verification code!";
                return View();
            }
        }


    }
}