using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace NetrunnerWebApp.Services
{
    public class Emailer : EmailService
    {
        public Task Email(Mail mail)
        {
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
            email.To.Add(mail.Recipient);
            email.From = new MailAddress("undergroundnetrunnerleague@gmail.com", mail.Header, System.Text.Encoding.UTF8);
            email.Subject = mail.Subject;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = (mail.Body);
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("undergroundnetrunnerleague@gmail.com", "neverever9");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(email);
            return Task.FromResult(true);
        }
    }
}