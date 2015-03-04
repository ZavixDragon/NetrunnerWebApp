using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using NetrunnerWebApp.Services;
using Rhino.Mocks;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Tests.Services
{
    [TestClass]
    public class UserAccountEmailerTest
    {
        private EmailService _emailServiceStub;
        private UserAccountEmailer emailer;
        private string _testEmail;
        private string _testUsername;
        private string _newPassword;

        [TestInitialize]
        public void init()
        {
            _emailServiceStub = MockRepository.GenerateStub<EmailService>();
            emailer = new UserAccountEmailer(_emailServiceStub);
            _testEmail = "email";
            _testUsername = "username";
            _newPassword = "password";
        }

        [TestMethod]
        public async Task UserAccountEmailer_EmailUsername_MethodCalled()
        {
            await emailer.EmailUsername(_testEmail, _testUsername);

            _emailServiceStub.AssertWasCalled(s => s.Email(Arg<Mail>.Is.Anything));
        }

        [TestMethod]
        public async Task UserAccountEmailer_EmailNewPassword_MethodCalled()
        {
            await emailer.EmailNewPassword(_testEmail, _newPassword);

            _emailServiceStub.AssertWasCalled(s => s.Email(Arg<Mail>.Is.Anything));
        }
    }
}
