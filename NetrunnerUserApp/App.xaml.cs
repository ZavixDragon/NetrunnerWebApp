using DomainObjects;
using NetrunnerUserApp.Entities;
using NetrunnerUserApp.Gateway;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using NetrunnerUserApp.PresentationLayer.Views;
using NetrunnerUserApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace NetrunnerUserApp
{
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainWindow();
            GatewayService gateway = new NetrunnerGateway();
            IoService fileService = new XmlIoService();
            CardUpdater updater = new CardUpdater(gateway, fileService);
            await updater.Initialize();
            ImageProvider imageProvider = new EmbeddedResourceImageProvider();
            InputValidaterService validater = new UserInputValidater();
            List<ViewModelCommandMode> commandModes = new List<ViewModelCommandMode>();
            commandModes.Add(new LoginCommandMode());
            commandModes.Add(new SignUpCommandMode());
            commandModes.Add(new ResetPasswordCommandMode());
            commandModes.Add(new RecoverUsernameCommandMode());
            ViewManagerService viewManager = new EnhancedViewFactory(imageProvider, gateway, validater, updater, window, commandModes);
            window.Show();
            if (updater.AmountToUpdate > 0)
                await viewManager.NavigateTo(ViewFactoryKeys.UpdatingPage);
            else
                await viewManager.NavigateTo(ViewFactoryKeys.AdmittancePage);
        }
    }
}