using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface UserAccountDatabaseService
    {
        Task<UserAccount> GetAccountInfo(string username);
        Task<UserAccount> GetAccountInfoFromEmail(string email);
        Task<bool> IsUsernameNotTaken(string username);
        Task<bool> IsEmailNotInUse(string email);
        Task AddNewAccount(UserAccount userApplicant);
        Task ChangePassword(UserAccount currentUser, string newPassword);
    }
}