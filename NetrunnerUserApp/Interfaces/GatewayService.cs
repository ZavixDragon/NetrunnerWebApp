using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Interfaces
{
    public interface GatewayService
    {
        Task<T> MakeRequest<T>(object parameter, string commandRoute);
    }
}
