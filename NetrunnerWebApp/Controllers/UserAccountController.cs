using DomainObjects;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace NetrunnerWebApp.Controllers
{
    public class UserAccountController : ApiController
    {

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

        [Route(Routes.SignUp)]
        public async Task<HttpResponseMessage> RegisterAccount(UserAccount userApplicant)
        {
            if (await AccountDBService.IsEmailTaken(userApplicant.Email))
                return StringResponse(SystemMessages.EmailAlreadyTaken);
            if (await AccountDBService.IsUsernameTaken(userApplicant.Username))
                return StringResponse(SystemMessages.UsernameAlreadyTaken);
            await AccountDBService.AddNewAccount(userApplicant);
            return StringResponse(SystemMessages.SuccessfulRegister);
        }

        [Route(Routes.RecoverUsername)]
        public async Task<HttpResponseMessage> RecoverUsername(object email)
        {
            var currentUser = await AccountDBService.GetAccountInfoFromEmail(email as string);
            if (currentUser.Username != null)
            {
                await Emailer.EmailUsername(email as string, currentUser.Username);
                return StringResponse(SystemMessages.UsernameRecovered);
            }
            return StringResponse(SystemMessages.NonExistentEmail);
        }

        [Route(Routes.ResetPassword)]
        public async Task<HttpResponseMessage> GenerateNewPassword(object email)
        {
            var currentUser = await AccountDBService.GetAccountInfoFromEmail(email as string);
            if (currentUser.Username != null)
            {
                string newPassword = await PasswordGenerator.GeneratePassword();
                await AccountDBService.ChangePassword(currentUser, newPassword);
                await Emailer.EmailNewPassword(email as string, newPassword);
                return StringResponse(SystemMessages.PasswordWasReset);
            }
            return StringResponse(SystemMessages.NonExistentEmail);
        }

        [Route(Routes.ChangePassword)]
        public async Task<HttpResponseMessage> ChangePassword(UserAccount currentUser, object newPassword)
        {
            var actualUser = await AccountDBService.GetAccountInfo(currentUser.Username);
            if (Authenticator.IsPasswordAndUsernameCorrect(currentUser, actualUser))
            {
                await AccountDBService.ChangePassword(currentUser, newPassword as string);
                return StringResponse(SystemMessages.PasswordUpdated);
            }
            return StringResponse(SystemMessages.InvalidPassword);
        }

        [Route(Routes.Login)]
        public async Task<HttpResponseMessage> AttemptLogin(UserAccount loginAttempt)
        {
            var actualUser = await AccountDBService.GetAccountInfo(loginAttempt.Username);
            if (Authenticator.IsPasswordAndUsernameCorrect(loginAttempt, actualUser))
                return StringResponse(loginAttempt.Username);
            return StringResponse(SystemMessages.InvalidLogin);
        }

        private HttpResponseMessage StringResponse(string value)
        {
            return new HttpResponseMessage { Content = new ObjectContent<StringObject>(new StringObject(value), new JsonMediaTypeFormatter()) };
        }
    }
}
