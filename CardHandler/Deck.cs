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


        // TODO: once the deck goes below a specific number, it should refill itself automatically.
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

        /// <summary>
        /// Removes a card from the deck at a given index.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>Card at the given index.</returns>
        public Card PopAt(int i)
        {
            Card c = GetAt(i);
            deckList.RemoveAt(i);
            return c;
        }
    }
}
