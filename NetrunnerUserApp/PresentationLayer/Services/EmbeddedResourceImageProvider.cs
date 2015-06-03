using NetrunnerUserApp.PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Services
{
    public class EmbeddedResourceImageProvider : ImageProvider
    {
        public static class Images
        {
            public static Image Null
            {
                get { return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("NetrunnerUserApp.PresentationLayer.Resources.ImageNotFound.png")); }
            }
        }

        public Image GetImage(string name)
        {
            var paths = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(x => x.Contains(name));
            if (paths.Count() != 0)
            {
                paths.OrderBy(x => x.Length);
                return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(paths.First()));
            }
            else return Images.Null;
        }
    }
}
