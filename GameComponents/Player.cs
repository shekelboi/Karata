using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.CardHandler;

namespace Karata.GameComponents
{
    class Player
    {
        public Deck PlayerDeck { get; set; }

        public Player()
        {
            PlayerDeck = new Deck(Cards.DeckType.PlayerDeck);
        }
    }
}
