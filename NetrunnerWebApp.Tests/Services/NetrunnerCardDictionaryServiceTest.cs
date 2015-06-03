using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Services;
using System.Collections.Generic;
using DomainObjects;

namespace NetrunnerWebApp.Tests.Services
{
    [TestClass]
    public class NetrunnerCardDictionaryServiceTest
    {
        private NetrunnerCardDictionaryService dictionaryService;

        [TestInitialize]
        public void init()
        {
            dictionaryService = new NetrunnerCardDictionaryService();
        }

        [TestMethod]
        public void NetrunnerCardDictionaryService_GetAllKeys_ReturnsListOfStrings()
        {
            List<string> ids = dictionaryService.GetAllCardIds();

            Assert.IsNotNull(ids);
        }

        [TestMethod]
        public void NetrunnerCardDictionaryservice_GetAccountSiphonCard_ReturnsCard()
        {
            Card card = dictionaryService.GetCard("Account Siphon");

            Assert.IsNotNull(card);
        }
    }
}
