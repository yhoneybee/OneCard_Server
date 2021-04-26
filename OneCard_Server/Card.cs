using System;
using System.Collections.Generic;
using System.Text;

namespace OneCard_Server
{
    public class Card
    {
        public Card(bool iscolor, int symbol, int num)
        {
            IsColor = iscolor;
            Symbol = symbol;
            Num = num;
        }
        public bool IsColor { get; set; }
        public int Symbol { get; set; }
        public int Num { get; set; }
    }
}
