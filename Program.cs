using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karata.CardHandler;
using Karata.GameComponents;

namespace Karata
{
    class Program
    {
        static void Main(string[] args)
        {
            int players = 4, packs = 200;
            //do
            //{
            //    Console.WriteLine("Enter the number of players:");
            //} while (!int.TryParse(Console.ReadLine(), out players));
            //do
            //{
            //    Console.WriteLine("Enter the number of decks (minimum one per player is recommended):");
            //} while (!int.TryParse(Console.ReadLine(), out packs));
            GameHandler gh = new GameHandler(players, packs);
        }
    }
}
