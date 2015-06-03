using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetrunnerWebApp.Models;
using NetrunnerWebApp.Interfaces;
using Rhino.Mocks;

namespace NetrunnerWebApp.Tests.Controllers
{
    [TestClass]
    public class NetrunnerCollectionControllerTest
    {
        private CardService _cardServiceStub;
        private NetrunnerCollectionController controller;


        [TestInitialize]
        public void init()
        {
            _cardServiceStub = MockRepository.GenerateStub<CardService>();
            controller = new NetrunnerCollectionController(_cardServiceStub);
        }

        [TestMethod]
        public async Task NetrunnerCollectionController_CallGiveCardKeyList_CardDitionaryServiceCalled()
        {
            await controller.PullCardIdList("");

            _cardServiceStub.AssertWasCalled(s => s.GetAllCardIds());
        }

        [TestMethod]
        public async Task NetrunnerCollectionController_AskForCardsWithListOfKeys_CardDictionaryServiceCalled()
        {
            var result = await controller.PullCard("");

            _cardServiceStub.AssertWasCalled(s => s.GetCard(Arg<string>.Is.Anything));
        }
    }
}
