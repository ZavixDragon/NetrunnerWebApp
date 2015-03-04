using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Services
{
    public class UserAccountEmailer : UserAccountEmailService
    {
        private EmailService _emailer;

        public UserAccountEmailer(EmailService emailer)
        {
            _emailer = emailer;
        }

        public Task EmailUsername(string email, string username)
        {
            Mail mail = new Mail { Recipient = email, Header = "Netrunner Anarch League", Subject = "Username Recovery", Body = username += "<-- your username" };
            _emailer.Email(mail);
            return Task.FromResult(true);
        }

        public Task EmailNewPassword(string email, string password)
        {
            Mail mail = new Mail { Recipient = email, Header = "Netrunner Anarch League", Subject = "Password Reset", Body = password += "<-- your new password" };
            _emailer.Email(mail);
            return Task.FromResult(true);
        }
    }
}