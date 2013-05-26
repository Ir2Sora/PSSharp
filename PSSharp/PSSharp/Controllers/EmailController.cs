using System;
using System.Configuration;
using ActionMailer.Net.Mvc;
using PSSharp.Models;

namespace PSSharp.Controllers
{
    public class EmailController : MailerBase
    {
        public static void SendEmail(Suggestion model)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmail"]))
            {
                new EmailController().Send(model).Deliver();
            }
        }

        private EmailResult Send(Suggestion model)
        {
            To.Add(model.User.Email);

            From = ConfigurationManager.AppSettings["Email"];

            Subject = ConfigurationManager.AppSettings["EmailSubject"];

            return Email("EmailView", model);
        }
    }
}