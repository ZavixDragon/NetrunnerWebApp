using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Interfaces
{
    public interface ViewContainerService
    {
        object GetView(string viewName);
        void Register(object view, string name);
    }
}
