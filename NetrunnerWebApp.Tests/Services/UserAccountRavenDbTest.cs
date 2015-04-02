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
        public async Task UserAccountRavenDb_DeleteAccount_ReturnNull()
        {
            await AddNewAccount();
            await DeleteAccount();
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountInfo_ReturnAddedAccount()
        {
            await AddNewAccount();
            UserAccount user = await ravenDb.GetAccountInfo(username);

            Assert.IsNotNull(user);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_GetAccountinfoFromEmail_ReturnAddedAccount()
        {
            await AddNewAccount();
            UserAccount user = await ravenDb.GetAccountInfoFromEmail(email);

            Assert.IsNotNull(user);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsUsernameNotTaken_ReturnFalse()
        {
            await AddNewAccount();
            bool result = await ravenDb.IsUsernameNotTaken(username);

            Assert.IsFalse(result);
            await DeleteAccount();
        }

        [TestMethod]
        public async Task UserAccountRavenDb_IsEmailNotInUse_ReturnFalse()
        {
            await AddNewAccount();
            bool result = await ravenDb.IsEmailNotInUse(email);

            Assert.IsFalse(result);
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
