using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Services;
using NetrunnerWebApp.Models;
using System.Threading.Tasks;
using DomainObjects;

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

            Assert.IsNull(NonExistentUser.Username);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfoFromEmail_ReturnNull()
        {
            UserAccount NonExistentUser = await ravenDb.GetAccountInfoFromEmail(email);

            Assert.IsNull(NonExistentUser.Username);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsUsernameTaken_ReturnFalse()
        {
            bool result = await ravenDb.IsUsernameTaken(username);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsEmailTaken_ReturnFalse()
        {
            bool result = await ravenDb.IsEmailTaken(email);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_DeleteAccount_ReturnNull()
        {
            await AddNewAccount();
            await DeleteAccount();
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNull(user.Username);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfo_ReturnAddedAccount()
       {
            await AddNewAccount();
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNotNull(user.Username);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountinfoFromEmail_ReturnAddedAccount()
        {
            await AddNewAccount();
            UserAccount user = await ravenDb.GetAccountInfoFromEmail(email);

            Assert.IsNotNull(user.Username);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsUsernameTaken_ReturnTrue()
        {
            await AddNewAccount();
            bool result = await ravenDb.IsUsernameTaken(username);

            Assert.IsTrue(result);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsEmailTaken_ReturnTrue()
        {
            await AddNewAccount();
            bool result = await ravenDb.IsEmailTaken(email);

            Assert.IsTrue(result);
            await DeleteAccount();
        }

        private async Task AddNewAccount()
        {
            UserAccount user = new UserAccount { Username = username, Password = password, Email = email };
            await ravenDb.AddNewAccount(user);
        }

        private async Task DeleteAccount()
        {
            await ravenDb.DeleteUserAccount(username);
        }

    }
}
