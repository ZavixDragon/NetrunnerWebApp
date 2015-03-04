using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface EmailService
    {
        Task Email(Mail mail);
    }
}