using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame
{
    public class Card
    {
        // suits and values
        public enum SUIT // enumeracja dla koloru kart
        {
            CLUBS,
            DIAMONDS,
            HEARTS,
            SPADES
        }
        public enum VALUE
        {
            TWO = 2, THREE, FOUR, FIVE, SIX, SEVEN,
            EIGHT, NINE, TEN, JOCK, QUEEN, KING, ACE
        }
        // wlasciwosci
        public SUIT MySuit { get; set; }
        public VALUE MyValue { get; set; }
    }
}
