using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.CardHandler;
using Karata.Cards;

namespace Karata.GameComponents
{
    class GameHandler
    {
        Tuple<Player, int> currentPlayer;

        Player[] players;

        GameDeck deck;

        bool gameOver
        {
            get
            {
                foreach (Player p in players)
                {
                    if (p.PlayerCards.Size == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

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

            currentPlayer = new Tuple<Player, int>(players[0], 0);
            
            StartGame();
        }

        public void StartGame()
        {
            Console.WriteLine("Your cards are the following:");

            foreach (Card c in currentPlayer.Item1.PlayerCards)
            {
                Console.WriteLine(c);
            }
            
            Console.WriteLine("Draw a card (d) or put a card down (number of the card)");
            Console.ReadLine();
            if (!gameOver)
            {
                NextPlayer();
                StartGame();
            }
            else
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Console.WriteLine("Game over!");
        }

        public void NextPlayer()
        {
            if (currentPlayer.Item2 == players.Length - 1)
            {
                currentPlayer = new Tuple<Player, int>(players[0], 0);
            }
            else
            {
                currentPlayer = new Tuple<Player, int>(players[currentPlayer.Item2 + 1], currentPlayer.Item2 + 1);
            }
        }
    }
}
