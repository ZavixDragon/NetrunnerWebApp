using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Controllers;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
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
        public async Task UserAccountController_RegisterAccountCorrectly_MethodsCalled()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameNotTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(true));
            _databaseServiceStub.Stub(s => s.IsEmailNotInUse(Arg<string>.Is.Anything)).Return(Task.FromResult(true));

            var response = await controller.PostRegistration(TestUserAccount);

            _databaseServiceStub.AssertWasCalled(s => s.IsUsernameNotTaken(testUsername));
            _databaseServiceStub.AssertWasCalled(s => s.IsEmailNotInUse(testEmail));
            _databaseServiceStub.AssertWasCalled(s => s.AddNewAccount(Arg<UserAccount>.Is.Anything));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_RegisterWithTakenUsername_ReturnErrorCode()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameNotTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(true));
            _databaseServiceStub.Stub(s => s.IsEmailNotInUse(Arg<string>.Is.Anything)).Return(Task.FromResult(false));

            var response = await controller.PostRegistration(TestUserAccount);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_RegisterWithUsedEmail_ReturnErrorCode()
        {
            _databaseServiceStub.Stub(s => s.IsUsernameNotTaken(Arg<string>.Is.Anything)).Return(Task.FromResult(false));
            _databaseServiceStub.Stub(s => s.IsEmailNotInUse(Arg<string>.Is.Anything)).Return(Task.FromResult(true));

            var response = await controller.PostRegistration(TestUserAccount);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_RecoverUsernameSuccessfully_MethodsCalled()
        {
            _databaseServiceStub.Stub(
                s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.PutUsernameInEmail(testEmail);

            _databaseServiceStub.AssertWasCalled(s => s.GetAccountInfoFromEmail(testEmail));
            _emailServiceStub.AssertWasCalled(s => s.EmailUsername(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_RecoverUsernameWithInvalidEmail_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));

            var response = await controller.PutUsernameInEmail(testEmail);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_ResetPasswordCorrectly_MethodsCalled()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.PostGeneratedPassword(testEmail);

            _databaseServiceStub.AssertWasCalled(s => s.GetAccountInfoFromEmail(testEmail));
            _emailServiceStub.AssertWasCalled(s => s.EmailNewPassword(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_ResetPasswordWithWrongEmail_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfoFromEmail(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));

            var response = await controller.PostGeneratedPassword(testEmail);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_ChangePasswordSuccessfully_MethodsCalled()
        {
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(true));
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(TestUserAccount));

            var response = await controller.PostNewPassword(TestUserAccount, newPassword);

            _databaseServiceStub.AssertWasCalled(s => s.GetAccountInfo(testUsername));
            _authenticatorServiceStub.AssertWasCalled(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything));
            _databaseServiceStub.AssertWasCalled(s => s.ChangePassword(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything));
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_ChangePasswordForNonExistentUser_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(false));

            var response = await controller.PostNewPassword(TestUserAccount, newPassword);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_LoginWithWrongInputs_ReturnErrorMessage()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(false));

            var response = await controller.GetInAccount(TestUserAccount);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [TestMethod]
        public async Task UserAccountController_LoginWithCorrectInputs_ReturnUserId()
        {
            _databaseServiceStub.Stub(s => s.GetAccountInfo(Arg<string>.Is.Anything)).Return(Task.FromResult(new UserAccount()));
            _authenticatorServiceStub.Stub(s => s.IsPasswordAndUsernameCorrect(Arg<UserAccount>.Is.Anything, Arg<UserAccount>.Is.Anything))
                .Return(Task.FromResult(true));

            var response = await controller.GetInAccount(TestUserAccount);
            string responseContent = await response.Content.ReadAsStringAsync();
            

            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
            Assert.AreEqual(testUsername, responseContent);
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

        private UserAccount TestUserAccount
        {
            get { return new UserAccount { Username = testUsername, Password = testPassword, Email = testEmail }; }
        }
    }
}