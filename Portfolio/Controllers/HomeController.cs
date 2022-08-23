using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("susanna.douglas@ethereal.email");
                mail.To.Add("susanna.douglas@ethereal.email");

                mail.Subject = sendMailDto.Subject;

                mail.IsBodyHtml = true;

                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Message : " + sendMailDto.Message;

                mail.Body = content;


                SmtpClient smtpClient = new SmtpClient("smtp.ethereal.email");

                NetworkCredential networkCredential = new NetworkCredential("susanna.douglas@ethereal.email", "mwJBsu9TxeeZkJWg3x");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 25;
                smtpClient.EnableSsl = false;
                smtpClient.Send(mail);
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }

            return Redirect("~/Home");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}