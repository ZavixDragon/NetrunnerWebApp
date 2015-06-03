using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetrunnerWebApp.Interfaces
{
    public interface CardService
    {
        List<string> GetAllCardIds();
        Card GetCard(string cardId);
    }
}