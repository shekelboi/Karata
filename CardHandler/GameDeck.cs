using Karata.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karata.CardHandler
{
    class GameDeck : Deck
    {
        public GameDeck(int numberOfDecks)
        {
            LoadCards(numberOfDecks);
        }

        public void LoadCards(int numberOfDecks)
        {

            for (int i = 0; i < numberOfDecks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 13; k++)
                    {
                        deckList.Add(new Card((CardName)k, (CardType)j));
                    }
                }

                deckList.Add(new Card(CardName.Joker, CardType.JokerBlack));
                deckList.Add(new Card(CardName.Joker, CardType.JokerRed));
            }

            deckList = deckList.OrderBy(x => r.Next()).ToList();
        }
    }
}
