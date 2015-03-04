using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface UserAccountAuthenticatorService
    {
        Task<bool> IsPasswordAndUsernameCorrect(UserAccount UserInput, UserAccount AccountInfo);
        Task<bool> IsPasswordAndEmailCorrect(UserAccount UserInput, UserAccount accountInfo);
    }
}