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
        void CardEffect(int num)
        {
            if (num == 1)
                InRoom.Attack(2);
            if (num == 2)
                InRoom.Attack(1);
            if (num == 11)
                InRoom.Jump();
            if (num == 13)
                InRoom.Again();
        }
        public bool Down(int symbol, int num)
        {
            LastDown = Find(symbol, num);
            bool isOk = false;
            if (num == 14)// joker
            {
                if (InRoom.LastCard.Symbol % 2 == 1)
                {
                    InRoom.Attack(4);
                    isOk = true;
                }
                else if (InRoom.LastCard.Symbol % 2 == 0)
                {
                    InRoom.Attack(6);
                    isOk = true;
                }
            }
            else
            {
                if (InRoom.AttackStack <= 1)
                {
                    if (InRoom.LastCard.Num == 14) // 마지막 카드가 조커라면
                    {
                        if (InRoom.LastCard.Symbol % 2 == 1) // 흑백
                        {
                            if (symbol % 2 == 1)
                            {
                                CardEffect(num);

                                isOk = true;
                            }
                        }
                        else if (InRoom.LastCard.Symbol % 2 == 0) // 컬러
                        {
                            if (symbol % 2 == 0)
                            {
                                CardEffect(num);

                                isOk = true;
                            }
                        }
                    }
                    else if (InRoom.LastCard.Symbol == symbol ||
                        InRoom.LastCard.Num == num)
                    {
                        CardEffect(num);

                        isOk = true;
                    }
                }
                else //공격스택이 쌓여있다면
                {
                    if (InRoom.LastCard.Num == 14)
                    {
                        if (InRoom.LastCard.Symbol % 2 == 1) // 흑백
                        {
                            if (symbol % 2 == 1)
                            {
                                switch (num)
                                {
                                    case 1:
                                        InRoom.Attack(2); isOk = true;
                                        break;
                                    case 2:
                                        InRoom.Attack(1); isOk = true;
                                        break;
                                    case 4:
                                        InRoom.Defence(); isOk = true;
                                        break;
                                    case 14:
                                        InRoom.Attack(4); isOk = true;
                                        break;
                                }
                            }
                        }
                        else if (InRoom.LastCard.Symbol % 2 == 0) // 컬러
                        {
                            if (symbol % 2 == 0)
                            {
                                switch (num)
                                {
                                    case 1:
                                        InRoom.Attack(2); isOk = true;
                                        break;
                                    case 2:
                                        InRoom.Attack(1); isOk = true;
                                        break;
                                    case 4:
                                        InRoom.Defence(); isOk = true;
                                        break;
                                    case 14:
                                        InRoom.Attack(6); isOk = true;
                                        break;
                                }
                            }
                        }
                    }
                    else if (InRoom.LastCard.Symbol == symbol ||
                    InRoom.LastCard.Num == num)
                    {
                        switch (num)
                        {
                            case 1:
                                InRoom.Attack(2); isOk = true;
                                break;
                            case 2:
                                InRoom.Attack(1); isOk = true;
                                break;
                            case 4:
                                InRoom.Defence(); isOk = true;
                                break;
                            case 14:
                                InRoom.Attack(4); isOk = true;
                                break;
                        }
                    }
                }
            }
            if (isOk)
            {
                Console.WriteLine($"Down!");
                InRoom.LastCard = LastDown;
                foreach (var p in InRoom.InPlayer)
                    Program.Proxy.LastCard(p.ID, RmiContext.ReliableSend, LastDown.Symbol, LastDown.Num);
                Cards.Remove(LastDown);
            }
            else
            {
                Console.WriteLine($"Draw! Count : {InRoom.AttackStack}");
                Draw();
            }
            InRoom.NextTurn();
            return isOk;
        }
        public void Draw()
        {
            Random rd = new Random();
            for (int i = 0; i < InRoom.AttackStack; i++)
            {
                Card card = new Card(rd.Next(1, 5), rd.Next(1, 15));
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
