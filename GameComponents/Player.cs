using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.CardHandler;

namespace Karata.GameComponents
{
    class Player
    {
        public Deck PlayerCards { get; set; }

        public Player()
        {
            PlayerCards = new Deck();
        }
    }
}
