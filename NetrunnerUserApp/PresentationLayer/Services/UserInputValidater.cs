using DomainObjects;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Services
{
    public class UserInputValidater : InputValidaterService
    {
        public async Task<string> ValidateSignUp(UserAccount SignUp)
        {
            if (string.IsNullOrEmpty(SignUp.Username) || string.IsNullOrEmpty(SignUp.Password) || string.IsNullOrEmpty(SignUp.Email))
                return SystemMessages.IncompleteSignUp;
            if (SignUp.Password.Length < 6 || !SignUp.Password.Any(Char.IsLetter) || !SignUp.Password.Any(Char.IsNumber))
                return SystemMessages.InsecurePassword;
            if (!SignUp.Email.Contains("@") || SignUp.Email.Count(x => x == '@') > 1 || SignUp.Email.First() == '@' || IsEmailSiteInvalid(SignUp.Email))
                return SystemMessages.InvalidEmail;
            return "";
        }

        private bool IsEmailSiteInvalid(string email)
        {
            int startIndex = email.IndexOf("@");
            string emailSite = email.Substring(startIndex + 1);
            if (!emailSite.Contains(".")  || emailSite.Count(x => x == '.') > 1 || emailSite.Last() == '.' || emailSite.First() == '.')
                return true;
            return false;
        }
    }
}
