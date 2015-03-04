using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Services
{
    public class UserAccountAuthenticator : UserAccountAuthenticatorService
    {
        public Task<bool> IsPasswordAndUsernameCorrect(UserAccount UserInput, UserAccount AccountInfo)
        {
            if (AccountInfo.Username != null)
                return Task.FromResult(UserInput.Username == AccountInfo.Username && UserInput.Password == AccountInfo.Password);
            return Task.FromResult(false);
        }

        public Task<bool> IsPasswordAndEmailCorrect(UserAccount UserInput, UserAccount AccountInfo)
        {
            if (AccountInfo.Username != null)
                return Task.FromResult(UserInput.Email == AccountInfo.Email && UserInput.Password == AccountInfo.Password);
            return Task.FromResult(false);
        }
    }
}