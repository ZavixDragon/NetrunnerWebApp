using DomainObjects;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Services
{
    public class UserAccountAuthenticator : UserAccountAuthenticatorService
    {
        public bool IsPasswordAndUsernameCorrect(UserAccount UserInput, UserAccount AccountInfo)
        {
            if (AccountInfo.Username != null)
                return UserInput.Username == AccountInfo.Username && UserInput.Password == AccountInfo.Password;
            return false;
        }

        public bool IsPasswordAndEmailCorrect(UserAccount UserInput, UserAccount AccountInfo)
        {
            if (AccountInfo.Username != null)
                return UserInput.Email == AccountInfo.Email && UserInput.Password == AccountInfo.Password;
            return false;
        }
    }
}