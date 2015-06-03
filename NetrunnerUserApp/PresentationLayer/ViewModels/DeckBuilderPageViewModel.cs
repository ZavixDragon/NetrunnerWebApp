using DomainObjects;
using NetrunnerUserApp.Entities;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerWebApp.PresetnationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NetrunnerUserApp.PresentationLayer.ViewModels
{
    public class DeckBuilderPageViewModel : NotificationObject
    {
        private ImageProvider _imageProvider;
        private GatewayService _gateway;
        private ViewManagerService _viewManager;
        public List<Image> CardImages { get; set; }
        public string PreviousPageNumber { get; set; }
        public string PageNumber { get; set; }
        public string NextPageNumber { get; set; }
        public string NextNextPageNumber {get; set; }
        public string LastPageNumber { get; set; }
        private int _numberOfCardsPerPage = 50;

        public ICommand GoToPageCommand { get { return new AsyncDelegateCommand(GoToPage); } }
        
        public DeckBuilderPageViewModel(ImageProvider imageProvider, GatewayService gateway, ViewManagerService viewManager)
        {
            _imageProvider = imageProvider;
            _gateway = gateway;
            _viewManager = viewManager;
            CardImages = new List<Image>();
            PageNumber = "1";
            UpdateCardImages();
            GoToPage("1");
        }

        public Image TitleImage
        {
            get { return _imageProvider.GetImage("TitleImage"); }
        }

        public void UpdateCardImages()
        {
            UpdateLastPageNumber();
            CardImages.Clear();
            int counter = NetrunnerCardsRemaining();
            if (counter >= _numberOfCardsPerPage)
                counter = _numberOfCardsPerPage;
            for (int i = 0; i < counter; i++)
            {
                CardImages.Add(byteArrayToImage(NetrunnerCards.Cards[CurrentCard(i)].CardImage));
            }
            RaisePropertyChanged("CardImages");
        }
        
        private async Task GoToPage(object parameter)
        {
            ResetStrings();
            UpdatePageNumbers(Convert.ToInt32(parameter));
            UpdateCardImages();
        }

        private void ResetStrings()
        {
            PreviousPageNumber = "";
            NextPageNumber = "";
            NextNextPageNumber = "";
        }

        private void UpdatePageNumbers(int input)
        {
            if (input > 1)
                PreviousPageNumber = (input - 1).ToString();
            PageNumber = input.ToString();
            if (input < Convert.ToInt32(LastPageNumber))
                NextPageNumber = (input + 1).ToString();
            if (input + 1 < Convert.ToInt32(LastPageNumber))
                NextNextPageNumber = (input + 2).ToString();
            RaisePropertyChanged(new string[] { "PreviousPageNumber", "PageNumber", "NextPageNumber", "NextNextPageNumber" });
        }

        private void UpdateLastPageNumber()
        {
            LastPageNumber = Math.Ceiling((double)NetrunnerCards.Cards.Count / _numberOfCardsPerPage).ToString();
            if (LastPageNumber == "0")
                LastPageNumber = "1";
            RaisePropertyChanged("LastPageNumber");
        }

        private int CurrentCard(int i)
        {
            return ((Convert.ToInt32(PageNumber) - 1) * _numberOfCardsPerPage) + i;
        }

        private int NetrunnerCardsRemaining()
        {
            return NetrunnerCards.Cards.Count - ((Convert.ToInt32(PageNumber) - 1) * _numberOfCardsPerPage);
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
