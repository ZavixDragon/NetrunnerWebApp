using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using Rhino.Mocks;
using NetrunnerUserApp.PresentationLayer.Views;
using NetrunnerUserApp.PresentationLayer.ViewModels;

namespace NetrunnerUserApp.Tests.PresentationLayer.Services
{
    [TestClass]
    public class ViewManagerTest
    {
        private ViewContainer _manager;

        [TestInitialize]
        public void init()
        {
            _manager = new ViewContainer();
        }

        [TestMethod]
        public void ViewManager_GetViewLoginPage_ReturnNonNullPage()
        {
            _manager.Register(new LoginPage(null, null), "LoginPage");

            object view = _manager.GetView("LoginPage");

            Assert.AreNotEqual(_manager.NullPage, view);
        }

        [TestMethod]
        public void ViewManager_GetViewNonExistentPage_ReturnNullPage()
        {
            object view = _manager.GetView("NonExistentPage");

            Assert.AreEqual(_manager.NullPage, view);
        }
    }
}
