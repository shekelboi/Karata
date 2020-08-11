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

        // The card the player currently posesses.
        Card currentCard;

        // Mode for the next turn.
        NextTurn mode;

        bool clockWise;

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
            clockWise = true;

            mode = NextTurn.None;

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
            while (!gameOver)
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
                        continue;
                    }
                    else
                    {
                        currentCard = currentPlayer.PlayerCards.PopAt(result);
                    }
                }
                else
                {
                    continue;
                }

                NextPlayer();
            }

            GameOver();
        }

        public void GameOver()
        {
            Console.WriteLine("Game over!");
            Console.ReadKey(true);
        }

        public void NextPlayer()
        {
            if (clockWise)
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
            else
            {
                if (currentPlayerId == 0)
                {
                    currentPlayerId = players.Length - 1;
                }
                else
                {
                    currentPlayerId--;
                }
            }
        }

        public NextTurn NextMoveIsPossible()
        {
            if (currentCard == null)
            {
                return NextTurn.None;
            }
            else if (currentCard.Name == CardName.Ace)
            {

            }
            return NextTurn.None;
        }
    }
}
