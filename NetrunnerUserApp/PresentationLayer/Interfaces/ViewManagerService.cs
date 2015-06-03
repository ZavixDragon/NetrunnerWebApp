using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Interfaces
{
    public interface ViewManagerService
    {
        Task NavigateTo(string pageName);
    }
}
