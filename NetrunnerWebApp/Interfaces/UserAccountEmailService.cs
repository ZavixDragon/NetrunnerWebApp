using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface UserAccountEmailService
    {
        Task EmailUsername(string email, string username);
        Task EmailNewPassword(string email, string password);
    }
}