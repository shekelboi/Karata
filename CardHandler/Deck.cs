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
        protected List<Card> deckList = new List<Card>();
       
        public int NumberOfDecks { get; }
        
        protected static Random r = new Random();

        public IEnumerator GetEnumerator()
        {
            return deckList.GetEnumerator();
        }

        public int Size { get { return deckList.Count; } }

        public Card Pop()
        {
            if (deckList.Count > 0)
            {
                Card c = deckList.First();
                deckList.RemoveAt(0);
                return c;
            }
            else
            {
                throw new Exception("No elements left to remove.");
            }
        }

        public void Push(Card c)
        {
            deckList.Add(c);
        }
        
        public Card GetAt(int i)
        {
            return deckList[i];
        }
    }
}
