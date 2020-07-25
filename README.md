# Karata
Karata is a famous game in Kenya. This is an implementation of that game in C#.

# Rules of Karata

Karata is played with a deck of French playing cards 52 cards + 2 jokers. The goal of the game is to be the first to get rid of all of your cards.

# Game description

Each player gets 4 cards. Each card has different functionalities.

Cards that do not have special functionalities are the following:

- Four
- Five
- Six
- Seven
- Nine
- Ten

Special cards include:

- Two: draw two cards.
- Three: draw three cards.
- Jack: "jump card", the next player if skipped (in case of two players the same player comes again).
- Eight/Queen: "question card", put down one more card ("the answer").
- King: "kickback" game goes on in reversed order (the previous player becomes the next player)
- Joker: draw five cards. Next player can put down any card that is of the same color as the joker.
- Ace: when put on card two, three or joker the player doesn't need to pick up any cards. The next player can put down any card. When placed on a non-sepcial card or an ace the player can choose any symbol that the next player has to put down.

Two can be placed on two, three can be placed on three and joker can be placed on joker. In this case, the next player has to draw the same amounts of cards accordingly.
