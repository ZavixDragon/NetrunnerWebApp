using DomainObjects;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Services
{
    public class CardUpdater : UpdaterService, IInitializable
    {
        private readonly GatewayService _gateway;
        private readonly IoService _ioService;
        private readonly string _filePath;
        private List<string> MissingCardIds = new List<string>();
        public double Progress { get; set; }
        public int AmountToUpdate;
        
        public CardUpdater(GatewayService gateway, IoService ioService)
        {
            _gateway = gateway;
            _ioService = ioService;
            Progress = 0;
            _filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _filePath = Path.Combine(_filePath, "NetrunnerCards.txt");
            LoadCardsFromFile();
        }

        public async Task Initialize()
        {
            await CalculateMissingCards();
        }

        private void LoadCardsFromFile()
        {
            try
            {
                NetrunnerCards.Cards = _ioService.LoadFromFile<List<Card>>(_filePath);
            }
            catch
            {
                NetrunnerCards.Cards = new List<Card>();
            }
        }

        private async Task CalculateMissingCards()
        {
            List<string> upToDateCardList = await _gateway.MakeRequest<List<string>>(null, Routes.PullAllCardIds);
            List<string> currentCardList = NetrunnerCards.Cards.Select(x => x.Title).ToList();
            MissingCardIds = upToDateCardList.Except(currentCardList).ToList();
            AmountToUpdate = MissingCardIds.Count();
        }

        public async Task Update()
        {
            NetrunnerCards.Cards.Add(await _gateway.MakeRequest<Card>(MissingCardIds[0], Routes.PullCard));
            MissingCardIds.RemoveAt(0);
            Progress = ((double)100 / (double)AmountToUpdate) * ((double)AmountToUpdate - (double)MissingCardIds.Count());
            _ioService.SaveToFile<List<Card>>(_filePath, NetrunnerCards.Cards);
        }
    }
}
