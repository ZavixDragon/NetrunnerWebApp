using DomainObjects;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetrunnerWebApp.Controllers
{
    public class NetrunnerCollectionController : ApiController
    {
        private CardService _dictionaryService;
        private JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();

        public NetrunnerCollectionController(CardService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [Route(Routes.PullAllCardIds)]
        public async Task<HttpResponseMessage> PullCardIdList(object parameter)
        {
            return new HttpResponseMessage { Content = new ObjectContent<List<string>>(_dictionaryService.GetAllCardIds(), formatter) };
        }

        [Route(Routes.PullCard)]
        public async Task<HttpResponseMessage> PullCard(object cardRequest)
        {
            return new HttpResponseMessage { Content = new ObjectContent<Card>(_dictionaryService.GetCard(cardRequest as string), formatter) };
        }
    }
}
