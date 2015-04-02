using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerUserApp.Tests.TestTools;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Tests.PresentationLayer.Services
{
    [TestClass]
    public class ImageProviderTest
    {
        private EmbeddedResourceImageProvider provider;

        [TestInitialize]
        public void init()
        {
            provider = new EmbeddedResourceImageProvider();
        }

        [TestMethod]
        public void ImageProvider_GetAllImages_ImagesFound()
        {
            AssertImage("TitleImage", isFound: true);
            AssertImage("LoginButtonImage", isFound: true);
            AssertImage("RegisterButtonImage", isFound: true);
        }

        [TestMethod]
        public async Task ImageProvider_GetNonExistentImage_ReturnErrorImage()
        {
            AssertImage("NonExistentImage", isFound: false);
        }

        [TestMethod]
        public async Task ImageProvider_GetTwoDifferentImages_ImagesAreNotEqual()
        {
            var image1 = await provider.GetImage("TitleImage");
            var image2 = await provider.GetImage("LoginButtonImage");

            Assert.AreNotEqual(ImageComparer.CompareResult.ciCompareOk, ImageComparer.Compare(image1, image2));
        }

        private async Task AssertImage(string name, bool isFound)
        {
            Assert.AreEqual(isFound, ImageComparer.CompareResult.ciCompareOk == ImageComparer.Compare(
                EmbeddedResourceImageProvider.Images.Null, await provider.GetImage(name)), 
                string.Format("Image '{0}' not found.", name));
        }
    }
}
