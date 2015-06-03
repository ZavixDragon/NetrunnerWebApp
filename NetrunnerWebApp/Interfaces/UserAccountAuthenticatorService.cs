using DomainObjects;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface UserAccountAuthenticatorService
    {
        bool IsPasswordAndUsernameCorrect(UserAccount UserInput, UserAccount AccountInfo);
        bool IsPasswordAndEmailCorrect(UserAccount UserInput, UserAccount accountInfo);
    }
}