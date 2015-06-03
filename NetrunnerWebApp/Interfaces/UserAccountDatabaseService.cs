using DomainObjects;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface UserAccountDatabaseService
    {
        Task<UserAccount> GetAccountInfo(string username);
        Task<UserAccount> GetAccountInfoFromEmail(string email);
        Task<bool> IsUsernameTaken(string username);
        Task<bool> IsEmailTaken(string email);
        Task AddNewAccount(UserAccount userApplicant);
        Task ChangePassword(UserAccount currentUser, string newPassword);
    }
}