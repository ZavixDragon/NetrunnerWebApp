using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Services;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Tests.Services
{
    [TestClass]
    public class UserAccountRavenDbTest
    {
        private UserAccountRavenDb ravenDb = new UserAccountRavenDb();
        private string username = "user";
        private string email = "email";
        private string password = "password";

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfo_ReturnNull()
        {
            UserAccount NonExistentUser = await ravenDb.GetAccountInfo(username);

            Assert.IsNull(NonExistentUser);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfoFromEmail_ReturnNull()
        {
            UserAccount NonExistentUser = await ravenDb.GetAccountInfoFromEmail(email);

            Assert.IsNull(NonExistentUser);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsUsernameNotTaken_ReturnTrue()
        {
            bool result = await ravenDb.IsUsernameNotTaken(username);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsEmailNotInUse_ReturnTrue()
        {
            bool result = await ravenDb.IsEmailNotInUse(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserAccountRavenDb_AddNewAccount()
        {
            UserAccount user = new UserAccount { Username = username, Password = password, Email = email };
            ravenDb.AddNewAccount(user);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfo_ReturnAddedAccount()
        {
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountinfoFromEmail_ReturnAddedAccount()
        {
            UserAccount user = await ravenDb.GetAccountInfoFromEmail(email);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsUsernameNotTaken_ReturnFalse()
        {
            bool result = await ravenDb.IsUsernameNotTaken(username);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsEmailNotInUse_ReturnFalse()
        {
            bool result = await ravenDb.IsEmailNotInUse(email);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_DeleteAccount_ReturnNull()
        {
            await ravenDb.DeleteUserAccount(username);
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNull(user);
        }
    }
}
