using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using NetrunnerUserApp.PresentationLayer.Views;
using NetrunnerUserApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NetrunnerUserApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewContainer viewContainer = new ViewContainer();
            viewContainer.Register(new LoginPage(new LoginPageViewModel(new EmbeddedResourceImageProvider()), viewContainer), "LoginPage");
            var window = new MainWindow(viewContainer);
            window.Show();
        }
    }
}