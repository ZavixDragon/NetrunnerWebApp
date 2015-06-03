using DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Tests.PresentationLayer.ViewModels
{
    [TestClass]
    public class DeckBuilderPageViewModelTest
    {
        private ImageProvider _imageProviderStub;
        private GatewayService _gatewayStub;
        private ViewManagerService _viewManagerStub;
        private Card _testCard = new Card();
        private List<Card> _testCards = new List<Card>();
        private DeckBuilderPageViewModel viewModel;

        [TestInitialize]
        public void init()
        {
            _imageProviderStub = MockRepository.GenerateStub<ImageProvider>();
            _gatewayStub = MockRepository.GenerateStub<GatewayService>();
            _viewManagerStub = MockRepository.GenerateStub<ViewManagerService>();
            _testCard = new Card();
            _testCards = new List<Card>();
            AddTestCards(199);
            viewModel = new DeckBuilderPageViewModel(_imageProviderStub, _gatewayStub, _viewManagerStub);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_BindTitleImage_ProviderWasCalled()
        {
            _imageProviderStub.Stub(s => s.GetImage(Arg<string>.Is.Anything)).Return(Resources.TestResources.TestImage);

            Assert.IsNotNull(viewModel.TitleImage);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_InitViewModel_CardsAreNotNull()
        {
            for (int i = 0; i < viewModel.CardImages.Count; i++)
            {
                Assert.IsNotNull(viewModel.CardImages[i]);
            }
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_GoToPage2_AllPageNumbersChanged()
        {
            viewModel.GoToPageCommand.Execute(2);

            Assert.AreEqual("1", viewModel.PreviousPageNumber);
            Assert.AreEqual("2", viewModel.PageNumber);
            Assert.AreEqual("3", viewModel.NextPageNumber);
            Assert.AreEqual("4", viewModel.NextNextPageNumber);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_GoToPage4OutOf4_AllPageNumbersChangedAndNextPageNumbersBlank()
        {
            viewModel.GoToPageCommand.Execute(4);

            Assert.AreEqual("3", viewModel.PreviousPageNumber);
            Assert.AreEqual("4", viewModel.PageNumber);
            Assert.AreEqual("", viewModel.NextPageNumber);
            Assert.AreEqual("", viewModel.NextNextPageNumber);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_GoToPage1_PreviousPageNumberBlank()
        {
            viewModel.GoToPageCommand.Execute(1);

            Assert.AreEqual("", viewModel.PreviousPageNumber);
            Assert.AreEqual("1", viewModel.PageNumber);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_GoToPage99_CardImagesCountEqualsZero()
        {
            viewModel.GoToPageCommand.Execute(99);

            Assert.AreEqual(0, viewModel.CardImages.Count);
        }

        [TestMethod]
        public void DeckBuilderPageViewModel_ChangeNumberOfNetrunnerCards_CalculateLastPageNumberCorrectly()
        {
            AddTestCards(1);
            viewModel.UpdateCardImages();

            Assert.AreEqual("1", viewModel.LastPageNumber);
        }

        private void AddTestCards(int NumberOfCards)
        {
            _testCards.Clear();
            _testCard.CardImage = PNGimageToByteArray(Resources.TestResources.TestImage);
            for(int i = 0; i < NumberOfCards; i++)
            {
                _testCards.Add(_testCard);
            }
            NetrunnerCards.Cards = _testCards;
        }

        public byte[] PNGimageToByteArray(Image imageIn)
        { 
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
