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

        GameDeck deck;

        public GameHandler(int numberOfPlayers, int numberOfDecks)
        {
            deck = new GameDeck(numberOfDecks);

            players = new Player[numberOfPlayers];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();

                for (int j = 0; j < 4; j++)
                {
                    players[i].PlayerCards.Push(deck.Pop());
                }
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine(players[i].PlayerCards.Pop());
                }
            }
        }
    }
}
