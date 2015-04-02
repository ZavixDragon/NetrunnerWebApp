using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using System.Drawing;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using Rhino.Mocks;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Tests.PresentationLayer.ViewModels
{
    [TestClass]
    public class LoginPageViewModelTest
    {
        private ImageProvider _imageProviderStub;
        private LoginPageViewModel viewModel;

        [TestInitialize]
        public void init()
        {
            _imageProviderStub = MockRepository.GenerateStub<ImageProvider>();
            viewModel = new LoginPageViewModel(_imageProviderStub);
        }

        [TestMethod]
        public void LoginPageViewModel_BindablesImages_ProviderCalled()
        {
            SetupImageProvider();

            Assert.IsNotNull(viewModel.TitleImage);
            Assert.IsNotNull(viewModel.LoginButtonImage);
            Assert.IsNotNull(viewModel.RegisterButtonImage);
        }

        private void SetupImageProvider()
        {
            _imageProviderStub.Stub(s => s.GetImage(Arg<string>.Is.Anything)).Return(GetTestImage());
        }

        private async Task<Image> GetTestImage()
        {
            return Resources.TestResources.TestImage;
        }
    }
}
