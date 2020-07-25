using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.CardHandler;

namespace Karata.GameComponents
{
    class GameHandler
    {
        Player[] players;

        Deck deck;

        public GameHandler(int numberOfPlayers)
        {
            deck = new Deck(Cards.DeckType.GameDeck);

            players = new Player[numberOfPlayers];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();

                for (int j = 0; j < 4; j++)
                {
                    players[i].PlayerDeck.Push(deck.Pop());
                }
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine(players[i].PlayerDeck.Pop());
                }
            }
        }
    }
}
