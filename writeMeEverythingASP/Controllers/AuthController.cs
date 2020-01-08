using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Http;
using writeMeEverythingASP.ApiModel;
using writeMeEverythingASP.Data;
using writeMeEverythingASP.Helper;
using writeMeEverythingASP.Models;
using writeMeEverythingASP.Res;

namespace writeMeEverythingASP.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly ChatContext _context;

        public AuthController()
        {
            _context = new ChatContext();
        }

        [HttpPost, Route("register")]
        public IHttpActionResult Register(Register register)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Users.Any(u => u.Email == register.Email))
            {
                ModelState.AddModelError("Error", Messages.EmailExists);
                return BadRequest(ModelState);
            }
            

            User user = new User()
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                Password = Crypto.HashPassword(register.Password),
                CreateAt = DateTime.Now,
                Verify = false,
                VerifyText = Guid.NewGuid().ToString(),
                isOnline = false,
                Lastseen = DateTime.Now

            };
            _context.Users.Add(user);
            _context.SaveChanges();


            var body = @"<div class=""""row""""><div class=""col-md-12""><table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #eaf0f7; margin: 0;"" bgcolor=""#eaf0f7"">
                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                        <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                                        <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                                            valign=""top"">
                                            <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 50px; 0"">
                                                <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px dashed #4d79f6;""
                                                       bgcolor=""#fff"">
                                                    <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                        <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                                            <meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                            <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <tr>
                                                                    <td><a href=""#""><img src=""./dist/media/svg/soho-logo.svg"" alt="""" style=""margin-left: auto; margin-right: auto; display:block; margin-bottom: 10px; height: 80px;""></a></td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #2dc57f; font-size: 24px; font-weight: 700; text-align: center; vertical-align: top; margin: 0; padding: 0 0 10px;""
                                                                        valign=""top"">{0}! Welcome to WriteMeEverything.com</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #3f5db3; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">Please confirm your email address by clicking the link below.</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">We may need to send you critical information about our service and it is important that we have an accurate email address.</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;""
                                                                        valign=""top""><a href=""{1}"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: block; border-radius: 5px; text-transform: capitalize; background-color: #2dc57f; margin: 0; border-color: #2dc57f; border-style: solid; border-width: 10px 20px;"">Confirm email address</a></td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; padding-top: 5px; vertical-align: top; margin: 0; text-align: right;"" valign=""top"">&mdash; <b>WriteMeEverything.com</b> - Discussion and Chat Application</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>";

            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.From = new MailAddress("yaxcioglan@mail.com");  // replace with valid value
            mailMessage.Subject = "Verify Account";
            //mailMessage.Body = string.Format(body, user.Firstname, "http://writemeeverything.com/Home/VerifyAccount?verifyText=" + user.VerifyText);
            mailMessage.Body = string.Format(body, user.Name, "https://localhost:44399/api/auth/verifyAcc?verifyText=" + user.VerifyText);
            mailMessage.IsBodyHtml = true;

            //send mail with smtp
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "",  // replace with valid value
                    Password = ""  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }

            var message = Messages.VerifyAcc + user.Email;
            return Ok(new { message });

        }

        [HttpGet, Route("verifyAcc")]
        public IHttpActionResult VerifyAcc(string verifyText)
        {
            User user = _context.Users.FirstOrDefault(u => u.VerifyText == verifyText);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            user.Verify = true;
            user.VerifyText = "Verified";
            _context.SaveChanges();

            var message = user.Name + ' ' + user.Surname + Messages.VerifyedAcc;
            return Ok(new { message });
        }

        [Auth]
        [HttpPost, Route("auth")]
        public IHttpActionResult Auth(Login login)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = _context.Users.FirstOrDefault(u => u.Email == login.Email);
            
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            if (!Crypto.VerifyHashedPassword(user.Password, login.Password))
            {
                ModelState.AddModelError("Error", Messages.WrongEmailOrPass);
                return BadRequest(ModelState);
            }
            if (!user.Verify)
            {
                ModelState.AddModelError("Error", Messages.VerifyAcc + user.Email);
                return BadRequest(ModelState);
            }

            user.Token = Guid.NewGuid().ToString();
            user.isOnline = true;
            user.Lastseen = DateTime.Now;

            _context.SaveChanges();

            var Token = user.Token;

            return Ok(new { Token });
            
        }

        [HttpPost, Route("resetPassword")]
        public IHttpActionResult ResetPassword(EmailModel email)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = _context.Users.FirstOrDefault(u => u.Email == email.email);

            if (user == null)
            {

                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            user.ResetText = Guid.NewGuid().ToString();

            _context.SaveChanges();

            // create mail
            var body = @"<div class=""""row""""><div class=""col-md-12""><table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #eaf0f7; margin: 0;"" bgcolor=""#eaf0f7"">
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                    <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                                    <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                                        valign=""top"">
                                        <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 50px; 0"">
                                            <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px dashed #4d79f6;""
                                                    bgcolor=""#fff"">
                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                    <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                                        <meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                            <tr>
                                                                <td><a href=""#""><img src=""./dist/media/svg/soho-logo.svg"" alt="""" style=""margin-left: auto; margin-right: auto; display:block; margin-bottom: 10px; height: 80px;""></a></td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #2dc57f; font-size: 24px; font-weight: 700; text-align: center; vertical-align: top; margin: 0; padding: 0 0 10px;""
                                                                    valign=""top"">{0}! Welcome to WriteMeEverything.com</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #3f5db3; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">Please to reset your password, follow the link.</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">We may need to send you critical information about our service and it is important that we have an accurate email address.</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;""
                                                                    valign=""top""><a href=""{1}"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: block; border-radius: 5px; text-transform: capitalize; background-color: #2dc57f; margin: 0; border-color: #2dc57f; border-style: solid; border-width: 10px 20px;"">Reset Password</a></td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; padding-top: 5px; vertical-align: top; margin: 0; text-align: right;"" valign=""top"">&mdash; <b>WriteMeEverything.com</b> - Discussion and Chat Application</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>";
            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.From = new MailAddress("yaxcioglan@mail.com");  // replace with valid value
            mailMessage.Subject = "Reset Password";
            // message.Body = string.Format(body, user.Firstname, "http://writemeeverything.com/Home/Reset?resetText=" + userR.ResetText);

            mailMessage.Body = string.Format(body, user.Name, "https://localhost:44399/api/auth/resetPassword?resetText=" + user.ResetText);
            mailMessage.IsBodyHtml = true;

            //send mail with smtp
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "",  // replace with valid value
                    Password = ""  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);

                string message = Messages.ResetPass + user.Email;
                return Ok(new { message });


            }

        }

        [HttpGet, Route("newPassword")]
        public IHttpActionResult NewPasswordGet([FromUri]string resetText)
        {
            User user = _context.Users.FirstOrDefault(u => u.ResetText == resetText);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            user.ResetText = Guid.NewGuid().ToString();
            _context.SaveChanges();

            var ResetToken = user.ResetText;
            return Ok(new { ResetToken });
        }

        [HttpPost, Route("newPassword")]
        public IHttpActionResult NewPasswordPost(ResetModel reset)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _context.Users.FirstOrDefault(u => u.ResetText == reset.resetToken);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }
            user.Password = Crypto.HashPassword(reset.password);
            user.ResetText = "";
            _context.SaveChanges();

            string message = Messages.NewPass;

            return Ok(new { message });
        }


        [Auth]
        [HttpGet, Route("logout")]
        public IHttpActionResult Logout()
        {

            string token = Request.Headers.GetValues("token").First().ToString();

            User user = _context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }
            user.Token = "";
            user.isOnline = false;
            user.Lastseen = DateTime.Now;
            _context.SaveChanges();

            string message = "OK";

            return Ok(new { message });
        }
    }
}
