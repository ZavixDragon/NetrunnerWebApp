using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Services;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerTracker.WebTest.Services
{
    [TestClass]
    public class AuthenticatorTest
    {
        private UserAccountAuthenticator authenticator;
        private UserAccount user;
        private UserAccount wrongUserInput;

        [TestInitialize]
        public void init()
        {
            authenticator = new UserAccountAuthenticator();
            user = new UserAccount { Username = "user", Password = "password", Email = "email" };
            wrongUserInput = new UserAccount { Username = "user", Password = "wrongPassword", Email = "email" };
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndUsernameCorrect_ReturnTrue()
        {
            bool check = await authenticator.IsPasswordAndUsernameCorrect(user, user);

            Assert.IsTrue(check);
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndUsernameCorrect_ReturnFalse()
        {
            bool check = await authenticator.IsPasswordAndUsernameCorrect(wrongUserInput, user);

            Assert.IsFalse(check);
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndUsernameCorrectEmptyUserAccount_ReturnFalse()
        {
            bool check = await authenticator.IsPasswordAndUsernameCorrect(user, new UserAccount());

            Assert.IsFalse(check);
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndEmailCorrect_ReturnTrue()
        {
            bool check = await authenticator.IsPasswordAndEmailCorrect(user, user);

            Assert.IsTrue(check);
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndEmailCorrect_ReturnFalse()
        {
            bool check = await authenticator.IsPasswordAndEmailCorrect(wrongUserInput, user);

            Assert.IsFalse(check);
        }

        [TestMethod]
        public async Task Authenticator_IsPasswordAndEmailCorrectEmptyUserAccount_ReturnFalse()
        {
            bool check = await authenticator.IsPasswordAndEmailCorrect(user, new UserAccount());

            Assert.IsFalse(check);
        }
    }
}