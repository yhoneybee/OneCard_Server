using System;
using System.Collections.Generic;
using System.Text;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Player
    {
        public Player(HostID id)
        {
            ID = id;
            InRoom = null;
            Cards = new List<Card>();
        }
        public void ReadySwitch(bool ready)
        {
            IsReady = ready;
            if (IsReady)
            {
                Console.WriteLine($"Client ({ID}) is Ready!");
                InRoom.AllReady();
            }
            else
            {
                Console.WriteLine($"Client ({ID}) is not Ready!");
            }
        }
        public void Down()
        {

        }
        public void Draw(int count)
        {
            Random rd = new Random();
            for (int i = 0; i < count; i++)
            {
                Card card = new Card(rd.Next(0, 1) == 1, rd.Next(1, 4), rd.Next(1, 15));
                Cards.Add(card);
                Program.Proxy.Draw(ID, RmiContext.ReliableSend, card.IsColor, card.Symbol, card.Num);
            }
        }
        public static List<Player> Players { get; set; } = new List<Player>();
        public HostID ID { get; set; }
        public Room InRoom { get; set; }
        public bool IsReady { get; set; }
        public List<Card> Cards { get; set; }
    }
}
