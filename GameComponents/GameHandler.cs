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
        int currentPlayerId = 0;

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
        Card lastCard = new Card();

        // Mode for the next turn.
        Draw mode = Draw.None;

        bool samePlayerComes = false;

        bool clockWise = true;

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

                // If the player cannot put down any card.
                bool playerMustDraw = true;

                foreach (Card card in currentPlayer.PlayerCards)
                {
                    if (ValidatePlayerCard(card))
                    {
                        playerMustDraw = false;
                        break;
                    }
                }

                if (input == "d" || playerMustDraw)
                {
                    int drawThisManyTimes = 0;
                    switch (mode)
                    {
                        case Draw.None:
                            drawThisManyTimes = 1;
                            break;
                        case Draw.Two:
                            drawThisManyTimes = 2;
                            break;
                        case Draw.Three:
                            drawThisManyTimes = 3;
                            break;
                        case Draw.Five:
                            drawThisManyTimes = 5;
                            break;
                    }

                    for (int i = 0; i < drawThisManyTimes; i++)
                    {
                        currentPlayer.PlayerCards.Push(deck.Pop());
                    }
                }
                else if (int.TryParse(input, out int result))
                {
                    if (result >= 0 || result < currentPlayer.PlayerCards.Size)
                    {
                        if (ValidatePlayerCard(currentPlayer.PlayerCards.GetAt(result)))
                        {
                            Card currentCard = currentPlayer.PlayerCards.PopAt(result);
                            switch (currentCard.Name)
                            {
                                case CardName.Ace:
                                    if (mode == Draw.None)
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
                                        currentCard = new Card(CardName.Ace, (CardType)chooseType);
                                    }
                                    else
                                    {
                                        mode = Draw.None;
                                        currentCard = new Card();
                                    }
                                    break;
                                case CardName.Two:
                                    mode = Draw.Two;
                                    break;
                                case CardName.Three:
                                    mode = Draw.Three;
                                    break;
                                case CardName.Jack:
                                    NextPlayer();
                                    break;
                                case CardName.Eight:
                                case CardName.Queen:
                                    samePlayerComes = true;
                                    break;
                                case CardName.King:
                                    clockWise = !clockWise;
                                    break;
                                case CardName.Joker:
                                    mode = Draw.Five;
                                    break;
                            }

                            // In all cases, the current card will be changed accordingly then be assigned to last card.
                            lastCard = currentCard;
                        }
                    }
                    else
                    {
                        // If the user input is incorrect, prompt the user again.
                        continue;
                    }
                }
                else
                {
                    // If the user input is incorrect, prompt the user again.
                    continue;
                }

                if (!samePlayerComes)
                {
                    NextPlayer();
                }
                else
                {
                    samePlayerComes = false;
                }
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
            // TODO: add validation rules
            if (mode == Draw.None)
            {

            }
            else
            {

            }
            return true;
        }
    }
}
