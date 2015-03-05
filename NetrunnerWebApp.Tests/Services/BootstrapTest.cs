using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerWebApp.Controllers;
using NetrunnerWebApp.Services;
using Microsoft.Practices.Unity;
using MSTestExtensions;

namespace NetrunnerWebApp.Tests.Services
{
    [TestClass]
    public class BootstrapTest
    {
        private IUnityContainer _container;

        [TestInitialize]
        public void init()
        {
            Bootstrap bootstrap = new Bootstrap();
            _container = bootstrap.GetContainer();
        }

        [TestMethod]
        public void Bootstrap_GetContainer_ContainerReturned()
        {
            Assert.IsNotNull(_container);
        }

        [TestMethod]
        public void Bootstrap_ResolveUserAccountController_ObjectNotNull()
        {
            var controller = _container.Resolve<UserAccountController>();

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Bootstrap_GrabUserAccountControllerTwice_IsSameInstance()
        {
            var controller = _container.Resolve<UserAccountController>();
            var controller2 = _container.Resolve<UserAccountController>();

            Assert.AreEqual(controller, controller2);
        }

        [TestMethod]
        public void Bootstrap_GrabNonRegisteredService_ThrowException()
        {
            ExceptionAssert.Throws<ResolutionFailedException>(() => _container.Resolve<UnregisteredService>());
        }

        public interface UnregisteredService { }
    }
}