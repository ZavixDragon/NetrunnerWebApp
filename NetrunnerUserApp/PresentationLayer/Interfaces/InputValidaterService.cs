using DomainObjects;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Interfaces
{
    public interface InputValidaterService
    {
        Task<string> ValidateSignUp(UserAccount SignUp);
    }
}
