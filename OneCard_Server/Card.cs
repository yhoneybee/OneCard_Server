using System;
using System.Collections.Generic;
using System.Text;

namespace OneCard_Server
{
    public class Card
    {
        public Card(int symbol, int num)
        {
            Symbol = symbol;
            Num = num;
        }
        public int Symbol { get; set; }
        public int Num { get; set; }
    }
}
