using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karata.Cards
{
    class Card
    {
        public Card(CardName name = CardName.Blank, CardType type = CardType.Blank)
        {
            this.Name = name;
            this.Type = type;
        }

        public CardName Name { get; }
        public CardType Type { get; }

        public override string ToString()
        {
            return Name + " - " + Type;
        }
    }
}
