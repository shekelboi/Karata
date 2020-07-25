using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.Cards;

namespace Karata.CardHandler
{
    class Deck : IEnumerable
    {
        List<Card> deck = new List<Card>();
        static Random r = new Random();

        public Deck(DeckType deckType)
        {
            switch (deckType)
            {
                case DeckType.GameDeck:
                    LoadCards();
                    break;
                case DeckType.PlayerDeck:
                    break;
                default:
                    break;
            }
        }
        
        public void LoadCards()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    deck.Add(new Card((CardName)j, (CardType)i));
                }
            }

            deck.Add(new Card(CardName.Joker, CardType.JokerBlack));
            deck.Add(new Card(CardName.Joker, CardType.JokerRed));

            deck = deck.OrderBy(x => r.Next()).ToList();

            Console.WriteLine(deck.Count);
        }

        public IEnumerator GetEnumerator()
        {
            return deck.GetEnumerator();
        }

        public int Size { get { return deck.Count; } }

        public Card Pop()
        {
            if (deck.Count > 0)
            {
                Card c = deck.First();
                deck.RemoveAt(0);
                return c;
            }
            else
            {
                throw new Exception("No elements left to remove.");
            }
        }

        public void Push(Card c)
        {
            deck.Add(c);
        }
    }
}
