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
            MyTurn = false;
            Cards = new List<Card>();
        }
        public static Player Find(HostID ID)
        {
            foreach (var player in Players)
                if (player.ID == ID)
                    return player;
            return null;
        }
        public Card Find(int symbol, int num)
        {
            foreach (var card in Cards)
                if (card.Symbol == symbol && card.Num == num)
                    return card;
            return null;
        }
        public void ReadySwitch(bool ready)
        {
            if (InRoom != null)
            {
                IsReady = ready;
                if (IsReady)
                {
                    Console.WriteLine($"Client ({ID}) is Ready!");
                    InRoom.AllReady(ID);
                }
                else
                {
                    Console.WriteLine($"Client ({ID}) is not Ready!");
                }
            }
        }
        public bool Down(int symbol, int num)
        {
            LastDown = Find(symbol, num);
            bool isOk = false;
            if (num == 14)// joker
            {
                if (InRoom.LastCard.Symbol % 2 == 1)
                {
                    InRoom.Attack(5);
                    isOk = true;
                }
                else if (InRoom.LastCard.Symbol % 2 == 0)
                {
                    InRoom.Attack(7);
                    isOk = true;
                }
            }
            else
            {
                if (InRoom.AttackStack <= 1)
                {
                    if (InRoom.LastCard.Symbol == symbol ||
                        InRoom.LastCard.Num == num)
                    {
                        if (num == 4)
                            InRoom.Defence();
                        if (num == 11)
                            InRoom.Jump();
                        if (num == 13)
                            InRoom.Again();

                        InRoom.NextTurn();

                        isOk = true;
                    }
                }
                else
                    Draw();
            }
            if (isOk)
            {
                InRoom.LastCard = LastDown;
                foreach (var p in InRoom.InPlayer)
                    Program.Proxy.LastCard(p.ID, RmiContext.ReliableSend, LastDown.Symbol, LastDown.Num);
                Cards.Remove(LastDown);
            }
            return isOk;
        }
        public void Draw()
        {
            Random rd = new Random();
            for (int i = 0; i < InRoom.AttackStack; i++)
            {
                Card card = new Card(rd.Next(1, 4), rd.Next(1, 14));
                Cards.Add(card);
                Program.Proxy.Draw(ID, RmiContext.ReliableSend, card.Symbol, card.Num);
            }
            InRoom.AttackStack = 1;
        }
        public static List<Player> Players { get; set; } = new List<Player>();
        public HostID ID { get; set; }
        public Room InRoom { get; set; }
        public bool IsReady { get; set; }
        public bool MyTurn { get; set; }
        public List<Card> Cards { get; set; }
        public Card LastDown { get; set; }
    }
}
