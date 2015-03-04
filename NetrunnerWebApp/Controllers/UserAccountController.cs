using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetrunnerWebApp.Controllers
{
    public class UserAccountController : ApiController
    {
        public int GetAll()
        {
            return 9;
        }

        private UserAccountDatabaseService AccountDBService;
        private UserAccountEmailService Emailer;
        private UserAccountAuthenticatorService Authenticator;
        private RandomPasswordGeneratorService PasswordGenerator;

        public UserAccountController(UserAccountDatabaseService databaseService, 
                                     UserAccountEmailService emailService, 
                                     UserAccountAuthenticatorService authenticatorService, 
                                     RandomPasswordGeneratorService randomPasswordGeneratorService)
        {
            AccountDBService = databaseService;
            Emailer = emailService;
            Authenticator = authenticatorService;
            PasswordGenerator = randomPasswordGeneratorService;
        }

        public async Task<HttpResponseMessage> PostRegistration(UserAccount userApplicant)
        {
            if (await AccountDBService.IsUsernameNotTaken(userApplicant.Username) && 
                await AccountDBService.IsEmailNotInUse(userApplicant.Email))
            {
                await AccountDBService.AddNewAccount(userApplicant);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        public async Task<HttpResponseMessage> PutUsernameInEmail(string email)
        {
            UserAccount currentUser = await AccountDBService.GetAccountInfoFromEmail(email);
            if (currentUser.Username != null)
            {
                await Emailer.EmailUsername(email, currentUser.Username);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        public async Task<HttpResponseMessage> PostGeneratedPassword(string email)
        {
            UserAccount currentUser = await AccountDBService.GetAccountInfoFromEmail(email);
            if(currentUser.Username != null)
            {
                string newPassword = await PasswordGenerator.GeneratePassword();
                await AccountDBService.ChangePassword(currentUser, newPassword);
                await Emailer.EmailNewPassword(email, newPassword);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        public async Task<HttpResponseMessage> PostNewPassword(UserAccount currentUser, string newPassword)
        {
            UserAccount actualUser = await AccountDBService.GetAccountInfo(currentUser.Username);
            if(await Authenticator.IsPasswordAndUsernameCorrect(currentUser, actualUser))
            {
                await AccountDBService.ChangePassword(currentUser, newPassword);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }
    }
}
