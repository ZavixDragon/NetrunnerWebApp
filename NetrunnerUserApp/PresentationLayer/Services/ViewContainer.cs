using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace NetrunnerUserApp.PresentationLayer.Services
{
    public class ViewContainer : ViewContainerService
    {
        public NullPage NullPage;
        private Dictionary<string, object> _views;

        public ViewContainer()
        {
            _views = new Dictionary<string, object>();
            NullPage = new NullPage();
        }

        public object GetView(string viewName)
        {
            object view;
            _views.TryGetValue(viewName, out view);
            return (view ?? NullPage);
        }

        public void Register(object view, string name)
        {
            _views.Add(name, view);            
        }
    }
}
