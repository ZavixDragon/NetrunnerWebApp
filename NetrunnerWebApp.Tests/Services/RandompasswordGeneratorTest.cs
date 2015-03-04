using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Tests.Services
{
    [TestClass]
    public class RandomPasswordGeneratorTest
    {
        private RandomPasswordGenerator passwordGenerator;

        [TestInitialize]
        public void init()
        {
            passwordGenerator = new RandomPasswordGenerator();
        }

        [TestMethod]
        public async Task RandomPasswordGenerator_GeneratePassword_StringNotNullOrEmpty()
        {
            string generatedPassword = await passwordGenerator.GeneratePassword();

            Assert.IsNotNull(generatedPassword);
            Assert.AreNotEqual("", generatedPassword);
        }

        [TestMethod]
        public async Task RandomPasswordGenerator_Generate100Passwords_NotAllIdentical()
        {
            List<string> generatedPasswords = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                generatedPasswords.Add(await passwordGenerator.GeneratePassword());
            }
            generatedPasswords.RemoveAll(s => s == generatedPasswords[0]);
            Assert.AreNotEqual(0, generatedPasswords.Count);
        }
    }
}