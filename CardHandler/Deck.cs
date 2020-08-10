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
        protected List<Card> deck = new List<Card>();

        public int NumberOfDecks { get; }


        protected static Random r = new Random();

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
