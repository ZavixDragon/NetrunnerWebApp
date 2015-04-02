using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NetrunnerUserApp.PresentationLayer.Interfaces;

namespace NetrunnerUserApp.PresentationLayer.ViewModels
{
    public class LoginPageViewModel
    {
        private ImageProvider _imageProvider;

        public LoginPageViewModel(ImageProvider provider)
        {
            _imageProvider = provider;
        }

        public Image TitleImage
        {
            get { return _imageProvider.GetImage("TitleImage").Result; }
        }

        public Image LoginButtonImage
        {
            get { return _imageProvider.GetImage("LoginButtonImage").Result; }
        }

        public Image RegisterButtonImage
        {
            get { return _imageProvider.GetImage("RegisterButtonImage").Result; }
        }
    }
}
