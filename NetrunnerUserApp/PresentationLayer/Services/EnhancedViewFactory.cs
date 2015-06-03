using NetrunnerUserApp.Entities;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using NetrunnerUserApp.PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetrunnerUserApp.PresentationLayer.Services
{
    public class EnhancedViewFactory : ViewManagerService
    {
        private readonly ImageProvider _imageProvider;
        private readonly GatewayService _gateway;
        private readonly InputValidaterService _validater;
        private readonly UpdaterService _updater;
        private MainWindow _window;
        private readonly List<ViewModelCommandMode> _commandModes;

        public EnhancedViewFactory(ImageProvider imageProvider, GatewayService gateway, InputValidaterService validater,
                                   UpdaterService updater, MainWindow window, List<ViewModelCommandMode> commandModes)
        {
            _imageProvider = imageProvider;
            _gateway = gateway;
            _validater = validater;
            _updater = updater;
            _window = window;
            _commandModes = commandModes;
        }

        public async Task NavigateTo(string pageName)
        {
            _window.MainFrame.Content = await CreatePage(pageName);
        }

        private async Task<object> CreatePage(string viewName)
        {
            switch(viewName)
            {
                case ViewFactoryKeys.UpdatingPage:
                    return BuildUpdatingPage();
                case ViewFactoryKeys.AdmittancePage:
                    return await BuildAdmittancePage();
                case ViewFactoryKeys.DeckBuilderPage:
                    return BuildDeckBuilderPage();
                default:
                    return BuildNullPage();
            }
        }

        private object BuildUpdatingPage()
        {
            var viewModel = new UpdatingPageViewModel(this, _updater);
            viewModel.Initialize();
            return new UpdatingPage(viewModel);
        }

        private async Task<object> BuildAdmittancePage()
        {
            var viewModel = new AdmittancePageViewModel(_imageProvider, _gateway, this, _validater, _commandModes);
            await viewModel.Initialize();
            return new AdmittancePage(viewModel);
        }

        private object BuildDeckBuilderPage()
        {
            var viewModel = new DeckBuilderPageViewModel(_imageProvider, _gateway, this);
            return new DeckBuilderPage(viewModel);
        }

        private object BuildNullPage()
        {
            return new NullPage();
        }
    }
}
