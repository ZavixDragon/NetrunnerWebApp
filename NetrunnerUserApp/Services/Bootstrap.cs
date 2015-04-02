using Microsoft.Practices.Unity;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Services
{
    public class Bootstrap
    {
        private IUnityContainer myContainer = new UnityContainer();

        public IUnityContainer GetContainer()
        {
            myContainer.RegisterType<ImageProvider, EmbeddedResourceImageProvider>();
            return myContainer;
        }
    }
}
