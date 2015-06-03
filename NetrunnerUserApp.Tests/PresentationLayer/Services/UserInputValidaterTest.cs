using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainObjects;
using NetrunnerUserApp.PresentationLayer.Services;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Tests.PresentationLayer.Services
{
    [TestClass]
    public class UserInputValidaterTest
    {
        private UserInputValidater _validater { get; set; }
        private UserAccount testUser { get; set; }
        private string validUsername = "q";
        private string validPassword = "qqqqq1";
        private string validEmail = "q@q.q";
        private string tooShortPassword = "qqqq1";
        private string noNumberPassword = "qqqqqq";
        private string noLetterPassword = "111111";
        private string noAtSymbol = "q.q";
        private string multipleAtSymbols = "q@q@q.q";
        private string noNameBeforeAddress = "@q.q";
        private string noDotInSiteAddress = "q@q";
        private string multipleDotsInSiteAddress = "q@q.q.q";
        private string noExtensionAfterAddressDot = "q@q.";
        private string noSiteName = "q@.q";
        private string dotOnlyBeforeSiteAddress = "q.q@q";

        [TestInitialize]
        public void Init()
        {
            _validater = new UserInputValidater();
            testUser = new UserAccount();
        }

        [TestMethod]
        public async Task UserInputValidater_ValidInput_returnsEmptyString()
        {
            testUser.Username = validUsername;
            testUser.Password = validPassword;
            testUser.Email = validEmail;

            await AssertSignUpIsValid(testUser);
        }

        [TestMethod]
        public async Task UserInputValidater_BadUsernameInput_ReturnsErrorMessage()
        {
            testUser.Password = validPassword;
            testUser.Email = validEmail;

            await AssertSignUpIsNotValid(testUser, SystemMessages.IncompleteSignUp);
        }

        [TestMethod]
        public async Task UserInputValidater_BadPasswordInputs_ReturnsErrorMessage()
        {
            testUser.Username = validUsername;
            testUser.Email = validEmail;

            await AssertSignUpIsNotValid(testUser, SystemMessages.IncompleteSignUp);

            testUser.Password = tooShortPassword;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InsecurePassword);

            testUser.Password = noNumberPassword;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InsecurePassword);

            testUser.Password = noLetterPassword;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InsecurePassword);
        }

        [TestMethod]
        public async Task UserInputValidater_BadEmailInputs_ReturnsErrorMessage()
        {
            testUser.Username = validUsername;
            testUser.Password = validPassword;

            await AssertSignUpIsNotValid(testUser, SystemMessages.IncompleteSignUp);

            testUser.Email = noAtSymbol;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = multipleAtSymbols;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = noNameBeforeAddress;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = noDotInSiteAddress;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = multipleDotsInSiteAddress;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = noExtensionAfterAddressDot;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = noSiteName;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);

            testUser.Email = dotOnlyBeforeSiteAddress;
            await AssertSignUpIsNotValid(testUser, SystemMessages.InvalidEmail);
        }

        private async Task AssertSignUpIsNotValid(UserAccount userAccount, string ExpectedErrorMessage)
        {
            string errorMessage = await _validater.ValidateSignUp(userAccount);
            Assert.AreEqual(ExpectedErrorMessage, errorMessage);
        }

        private async Task AssertSignUpIsValid(UserAccount userAccount)
        {
            string errorMessage = await _validater.ValidateSignUp(userAccount);
            Assert.AreEqual("", errorMessage);
        }
    }
}
