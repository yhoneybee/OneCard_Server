using System;
using System.Collections.Generic;
using System.Text;
using Nettention.Proud;
using System.Threading;

namespace OneCard_Server
{
    public class Room
    {
        public Room(string name, int pin, int max)
        {
            Name = name;
            Pin = pin;
            Max = max;
            Next = 1;
            TurnIndex = 0;
            IsFirst = true;
            AttackStack = 1;
            TurnLoop = 1;
            InPlayer = new List<Player>();
        }
        public static bool Create(string name, int pin, int max)
        {
            foreach (var c in Rooms)
                if (c.Name == name)
                {
                    Console.WriteLine($"[ FAIL ] Name is already used");
                    return false;
                }
            Rooms.Add(new Room(name, pin, max));
            Console.WriteLine($"[  OK  ] Room Creation Successful");
            return true;
        }
        public static Room Find(string name)
        {
            foreach (var room in Rooms)
                if (room.Name == name)
                    return room;
            return null;
        }
        public bool Join(Player player)
        {
            if (InPlayer.Count < Max)
            {
                if (!IsStart)
                {
                    player.IsReady = false;
                    player.InRoom = this;
                    InPlayer.Add(player);
                    Console.WriteLine($"[  OK  ] Client ({player.ID}) ----> Room ({Name})");
                    return true;
                }
            }
            Console.WriteLine($"[ FAIL ] Client ({player.ID}) --x-> Room ({Name})");
            return false;
        }
        public bool Leave(Player player)
        {
            foreach (var p in InPlayer)
            {
                if (p.ID == player.ID)
                {
                    player.InRoom = null;
                    InPlayer.Remove(player);
                    Console.WriteLine($"[  OK  ] Client ({player.ID}) <---- Room ({Name})");
                    if (InPlayer.Count == 0)
                    {
                        Console.WriteLine($"[  OK  ] Room ({Name}) was Destroy");
                        Rooms.Remove(this);
                    }
                    return true;
                }
            }
            Console.WriteLine($"[ FAIL ] Client ({player.ID}) <-x-- Room ({Name})");
            return false;
        }
        public bool AllReady(HostID ID)
        {
            if (InPlayer.Count <= 1)
            {
                Console.WriteLine("[ Count <= 1 ] condition is true");
                return false;
            }
            foreach (var p in InPlayer)
                if (!p.IsReady)
                    return false;
            Console.WriteLine("All Player is Ready! Game Start!");
            IsStart = true;
            Random rd = new Random();
            foreach (var p in InPlayer)
            {
                Program.Proxy.Start(p.ID, RmiContext.ReliableSend);

                if (p.ID != ID)
                {
                    // 클라이언트 들에게 유니티에서 정보가 보이게 만들라고 명령
                }

                if (LastCard == null)
                    LastCard = new Card(rd.Next(1, 5), rd.Next(1, 15));

                Console.WriteLine($"{LastCard.Symbol} / {LastCard.Num}");
                Program.Proxy.LastCard(p.ID, RmiContext.ReliableSend, LastCard.Symbol, LastCard.Num);
            }
            return true;
        }
        public void ChangeSymbol(int symbol)
        {
            Console.Write("Change to ");
            switch (symbol)
            {
                case 1:
                    Console.WriteLine("Spade!");
                    break;
                case 2:
                    Console.WriteLine("Diamond!");
                    break;
                case 3:
                    Console.WriteLine("Clover!");
                    break;
                case 4:
                    Console.WriteLine("Heart!");
                    break;
            }
            LastCard.Symbol = symbol;
        }
        public int Reverse() => Next == 1 ? Next = -1 : Next = 1;
        public void Attack(int count)
        {
            Console.WriteLine($"Attack count : {count} += {AttackStack} ----> {count + AttackStack}");
            AttackStack += count;
        }
        public void Defence() => AttackStack = 0;
        public void Again() => TurnLoop = 0;
        public void Jump() => TurnLoop = 2;
        public void NextTurn()
        {
            for (int i = 0; i < TurnLoop; i++)
            {
                TurnIndex += Next;
                if (TurnIndex == InPlayer.Count)
                    TurnIndex = 0;
                if (TurnIndex == -1)
                    TurnIndex = InPlayer.Count - 1;
            }
            TurnLoop = 1;
        }
        public void Logic()
        {
            if (IsStart)
            {
                if (IsFirst)
                {
                    IsFirst = false;
                    Console.WriteLine($"{InPlayer[TurnIndex].ID} TURN!");
                    Program.Proxy.TurnStart(InPlayer[TurnIndex].ID, RmiContext.ReliableSend);
                    InPlayer[TurnIndex].MyTurn = true;
                }
                else
                    if (InPlayer.Count >= 1)
                        if (!InPlayer[TurnIndex].MyTurn)
                            IsFirst = true;
            }
        }
        public static List<Room> Rooms { get; set; } = new List<Room>();
        public string Name { get; set; }
        public int Pin { get; set; }
        public int Max { get; set; }
        public bool IsStart { get; set; }
        public List<Player> InPlayer { get; set; }
        public Card LastCard { get; set; }
        public int Next { get; set; }
        public int TurnIndex { get; set; }
        public int TurnLoop { get; set; }
        public bool IsFirst { get; set; }
        public int AttackStack { get; set; }
    }
}
