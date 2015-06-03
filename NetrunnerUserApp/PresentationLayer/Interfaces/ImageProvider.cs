using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Interfaces
{
    public interface ImageProvider
    {
        Image GetImage(string name);
    }
}
