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
        int currentPlayerId;

        Player currentPlayer
        {
            get
            {
                return players[currentPlayerId];
            }
        }

        Player[] players;

        GameDeck deck;

        // The card that the last player disposed of.
        Card lastCard;

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
                players[i] = new Player(i);

                for (int j = 0; j < 4; j++)
                {
                    players[i].PlayerCards.Push(deck.Pop());
                }
            }

            currentPlayerId = 0;

            StartGame();
        }

        public void StartGame()
        {
            Console.WriteLine("Curentplayer: {0}", currentPlayerId + 1);
            Console.WriteLine("Your cards are the following:");

            foreach (Card c in currentPlayer.PlayerCards)
            {
                Console.WriteLine(c);
            }
            

            Console.WriteLine("Draw a card (d) or put a card down (number of the card)");
            string input = Console.ReadLine();

            if (input == "d")
            {
                currentPlayer.PlayerCards.Push(deck.Pop());
            }
            else if (int.TryParse(input, out int result))
            {
                if (result < 0 || result >= currentPlayer.PlayerCards.Size)
                {
                    StartGame();
                }
                else
                {

                }
            }
            else
            {
                StartGame();
            }

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
            if (currentPlayerId == players.Length - 1)
            {
                currentPlayerId = 0;
            }
            else
            {
                currentPlayerId++;
            }
        }
    }
}
