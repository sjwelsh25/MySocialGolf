using MySocialGolf.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Manager.Email
{
    public class EmailManager
    {
        public bool SendEmail(EmailModel emailModel)
        {
            emailModel.ApiKey = ConfigurationManager.AppSettings["SendGridApiKey"].ToString();
            emailModel.FromEmailAddress = ConfigurationManager.AppSettings["SendGridFromEmailAddress"].ToString();
            emailModel.FromEmailName = ConfigurationManager.AppSettings["SendGridFromEmailName"].ToString();
            SendEmailTask(emailModel);
            return true;
        }

        private static async Task SendEmailTask(EmailModel emailModel)
        {
            // var apiKey = apiKey; // Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(emailModel.ApiKey);
            var from = new EmailAddress(emailModel.FromEmailAddress, emailModel.FromEmailName);
            var subject = emailModel.Subject;                //  "Sending with SendGrid is Fun " + DateTime.Now.ToString("yyyymmddhhnnss");
            var to = new EmailAddress(emailModel.ToEmailAddress, emailModel.ToEmailName);
            var plainTextContent = emailModel.BodyPlainText; // "and easy to do anywhere, even with C#";
            var htmlContent = emailModel.BodyHtml;           // "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
