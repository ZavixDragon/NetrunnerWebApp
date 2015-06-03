using DomainObjects;
using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetrunnerWebApp.Services
{
    public class NetrunnerCardDictionaryService : CardService
    {
        private NetrunnerCardDictionary dictionary = new NetrunnerCardDictionary();

        public List<string> GetAllCardIds()
        {
            return dictionary.Cards.Keys.ToList();
        }

        public Card GetCard(string cardId)
        {
            return dictionary.Cards[cardId];
        }
    }
}