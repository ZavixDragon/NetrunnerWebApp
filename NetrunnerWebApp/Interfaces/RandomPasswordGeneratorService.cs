using System.Threading.Tasks;

namespace NetrunnerWebApp.Interfaces
{
    public interface RandomPasswordGeneratorService
    {
        Task<string> GeneratePassword();
    }
}