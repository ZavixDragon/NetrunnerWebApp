using DomainObjects;
using NetrunnerUserApp.Entities;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.ViewModels
{
    public class UpdatingPageViewModel : NotificationObject, IInitializable
    {
        public double Progress { get; set; }
        private readonly ViewManagerService _viewManager;
        private readonly UpdaterService _updater;

        public UpdatingPageViewModel(ViewManagerService viewManager, UpdaterService updater)
        {
            _viewManager = viewManager;
            _updater = updater;
            Progress = 0;
        }

        public async Task Initialize()
        {
            await Update();
        }

        public async Task Update()
        {
            while (Progress < 100)
            {
                await _updater.Update();
                Progress = _updater.Progress;
                RaisePropertyChanged("Progress");
            }
            await _viewManager.NavigateTo(ViewFactoryKeys.AdmittancePage);
        }
    }
}
