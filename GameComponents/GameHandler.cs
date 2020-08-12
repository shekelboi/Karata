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

        // Mode for the next turn.
        Draw mode;

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

            lastCard = new Card();

            mode = Draw.None;

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

        private void StartGame()
        {
            while (!gameOver)
            {
                Console.WriteLine("Curentplayer: {0}", currentPlayerId + 1);
                Console.WriteLine("Current card: {0}", lastCard.Name);
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
                        if (ValidatePlayerCard(currentPlayer.PlayerCards.GetAt(result)))
                        {
                            Card currentCard = currentPlayer.PlayerCards.PopAt(result);
                            if (currentCard.Name == CardName.Ace && mode == Draw.None)
                            {
                                bool choiceValid = false;
                                int chooseType;
                                do
                                {
                                    Console.WriteLine("Hearts, spades, diamonds or clubs?");
                                    choiceValid = int.TryParse(Console.ReadLine(), out chooseType);
                                    if (choiceValid)
                                    {
                                        if (chooseType >= 0 && chooseType < 4)
                                        {
                                            choiceValid = true;
                                        }
                                    }
                                } while (!choiceValid);
                                lastCard = new Card(CardName.Ace, (CardType)chooseType);
                            }
                        }
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

        private void GameOver()
        {
            Console.WriteLine("Game over!");
            Console.ReadKey(true);
        }

        private void NextPlayer()
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

        private bool ValidatePlayerCard(Card c)
        {
            if (mode == Draw.None)
            {

            }
            else
            {

            }
            return false;
        }
    }
}
