using DomainObjects;
using NetrunnerUserApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NetrunnerUserApp.Gateway
{
    public class NetrunnerGateway : GatewayService
    {
        private HttpClient client = new HttpClient();

        public async Task<T> MakeRequest<T>(object parameter, string commandRoute)
        {
            var address = "Http://Localhost:60930/";
            address += commandRoute;
            var PostBody = JsonConvert.SerializeObject(parameter);
            var result = await client.PostAsync(address, new StringContent(PostBody, UnicodeEncoding.UTF8, "Application/Json"));
            var resultContent = await result.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<T>(resultContent);
        }
    }
}