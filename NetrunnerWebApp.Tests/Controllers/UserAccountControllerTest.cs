using DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Controllers;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using Newtonsoft.Json;
using Rhino.Mocks;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Tests.Controllers
{
    [TestClass]
    public class UserAccountControllerTest
    {
        private UserAccountDatabaseService _databaseServiceStub;
        private UserAccountEmailService _emailServiceStub;
        private UserAccountAuthenticatorService _authenticatorServiceStub;
        private RandomPasswordGeneratorService _randomPasswordGeneratorServiceStub;
        private UserAccountController controller;
        private string newPassword = "new";
        private string testUsername = "user";
        private string testEmail = "email";
        private string testPassword = "password";

        [TestInitialize]
        public void init()
        {
            _databaseServiceStub = MockRepository.GenerateStub<UserAccountDatabaseService>();
            _emailServiceStub = MockRepository.GenerateStub<UserAccountEmailService>();
            _authenticatorServiceStub = MockRepository.GenerateStub<UserAccountAuthenticatorService>();
            _randomPasswordGeneratorServiceStub = MockRepository.GenerateStub<RandomPasswordGeneratorService>();
            controller = new UserAccountController(
                _databaseServiceStub, _emailServiceStub, _authenticatorServiceStub, _randomPasswordGeneratorServiceStub);
            SetupBlankTasksToAvoidNullExceptions();
        }

        [TestMethod]
        public async Task UserAccountController_RegisterAccountCorrectly_AddNewAccountAndReturnsSuccessMessage()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _databaseServiceStub.Stub(s => s.IsEmailTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(false));

            var response = await controller.RegisterAccount(TestUserAccount);

            _databaseServiceStub.AssertWasCalled(s => s.AddNewAccount(Arg<UserAccount>.Is.Anything));
            await assertMessageCorrect(SystemMessages.SuccessfulRegister, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_RegisterWithTakenUsername_ReturnProperErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(true));
            _databaseServiceStub.Stub(s => s.IsEmailTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(false));

            var response = await controller.RegisterAccount(TestUserAccount);

            await assertMessageCorrect(SystemMessages.UsernameAlreadyTaken, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_RegisterWithTakenEmail_ReturnProperErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _databaseServiceStub.Stub(s => s.IsEmailTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(true));

            var response = await controller.RegisterAccount(TestUserAccount);

            await assertMessageCorrect(SystemMessages.EmailAlreadyTaken, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_RecoverUsernameSuccessfully_EmailerCalledAndSuccessMessage()
        {
            _databaseServiceStub.Stub(
                s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.RecoverUsername(testEmail);

            _emailServiceStub.AssertWasCalled(s => s.EmailUsername(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            await assertMessageCorrect(SystemMessages.UsernameRecovered, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_RecoverUsernameWithInvalidEmail_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));

            var response = await controller.RecoverUsername(testEmail);

            await assertMessageCorrect(SystemMessages.NonExistentEmail, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_ResetPasswordCorrectly_EmailerCalledAndSuccessMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.GenerateNewPassword(testEmail);

            _emailServiceStub.AssertWasCalled(s => s.EmailNewPassword(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            await assertMessageCorrect(SystemMessages.PasswordWasReset, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_ResetPasswordWithWrongEmail_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));

            var response = await controller.GenerateNewPassword(testEmail);

            await assertMessageCorrect(SystemMessages.NonExistentEmail, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_ChangePasswordSuccessfully_ChangePasswordCalledAndSuccessMessage()
        {
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(true));
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.ChangePassword(TestUserAccount, newPassword);

            _databaseServiceStub.AssertWasCalled(s => s.ChangePassword(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything));
            await assertMessageCorrect(SystemMessages.PasswordUpdated, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_ChangePasswordWithWrongPassword_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(false));

            var response = await controller.ChangePassword(TestUserAccount, newPassword);

            await assertMessageCorrect(SystemMessages.InvalidPassword, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_LoginWithWrongInputs_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(false));

            var response = await controller.AttemptLogin(TestUserAccount);

            await assertMessageCorrect(SystemMessages.InvalidLogin, await response.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task UserAccountController_LoginWithCorrectInputs_ReturnUserId()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(true));

            var response = await controller.AttemptLogin(TestUserAccount);

            await assertMessageCorrect(testUsername, await response.Content.ReadAsStringAsync());
        }

        private void SetupBlankTasksToAvoidNullExceptions()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));
            _databaseServiceStub.Stub(s => s.AddNewAccount(Arg<UserAccount>.Is.Anything)).Return(Task.FromResult(false));
            _databaseServiceStub.Stub(s => s.ChangePassword(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _emailServiceStub.Stub(s => s.EmailUsername(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _emailServiceStub.Stub(s => s.EmailNewPassword(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _randomPasswordGeneratorServiceStub.Stub(s => s.GeneratePassword()).Return(Task.FromResult(""));
        }

        private async Task assertMessageCorrect(string message, string JsonStringObject)
        {
            StringObject responseContent = await JsonConvert.DeserializeObjectAsync<StringObject>(JsonStringObject);
            Assert.AreEqual(message, responseContent.Value);
        }

        private UserAccount TestUserAccount
        {
            get { return new UserAccount { Username = testUsername, Password = testPassword, Email = testEmail }; }
        }
    }
}