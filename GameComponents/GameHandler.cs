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
                Console.WriteLine("Mode: {0}", mode);
                Console.WriteLine("Curentplayer: {0}", currentPlayerId + 1);
                Console.WriteLine("Current card: {0}", lastCard);
                Console.WriteLine("Your cards are the following:");


                for (int i = 0; i < currentPlayer.PlayerCards.Size; i++)
                {
                    Console.WriteLine("{0}. {1}", i, currentPlayer.PlayerCards.GetAt(i));
                }


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

                string input = "";

                if (!playerMustDraw)
                {
                    Console.WriteLine("Draw a card (d) or put a card down (number of the card)");
                    input = Console.ReadLine();
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
                    if (result >= 0 && result < currentPlayer.PlayerCards.Size)
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
                        else
                        {
                            samePlayerComes = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("VALIDATION FAILURE");
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
            // If the card is Ace or last card is blank, no matter what, it can be put down.
            if (lastCard.Type == CardType.Blank || c.Name == CardName.Ace)
            {
                return true;
            }
            // TODO: add validation rules
            if (mode == Draw.None)
            {
                // If they have the same type or value, they can be put on each other.
                if (lastCard.Type == c.Type || lastCard.Name == c.Name)
                {
                    return true;
                }
                // Joker can be put on any card of the same color and any card of the same color can be put on joker.
                else if (lastCard.Type == CardType.JokerBlack && (c.Type == CardType.Clubs || c.Type == CardType.Spades))
                {
                    return true;
                }
                else if (lastCard.Type == CardType.JokerRed && (c.Type == CardType.Diamonds || c.Type == CardType.Hearts))
                {
                    return true;
                }
                else if (c.Type == CardType.JokerRed && (lastCard.Type == CardType.Hearts || lastCard.Type == CardType.Diamonds))
                {
                    return true;
                }
                else if (c.Type == CardType.JokerBlack && (lastCard.Type == CardType.Clubs || lastCard.Type == CardType.Spades))
                {
                     return true;
                }

                return false;
            }
            else
            {
                return false;
                // TODO: implement drawing modes
            }
            return true;
        }
    }
}
