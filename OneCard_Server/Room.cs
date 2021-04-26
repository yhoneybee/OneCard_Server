using System;
using System.Collections.Generic;
using System.Text;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Room
    {
        public Room(string name, int pin, int max)
        {
            Name = name;
            Pin = pin;
            Max = max;
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
        public bool Join(Player player)
        {
            if (InPlayer.Count < Max)
            {
                player.IsReady = false;
                player.InRoom = this;
                InPlayer.Add(player);
                Console.WriteLine($"[  OK  ] Client ({player.ID}) ----> Room ({Name})");
                return true;
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
        public bool AllReady()
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
            foreach (var p in InPlayer)
                Program.Proxy.Start(p.ID, RmiContext.ReliableSend);
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
        public static List<Room> Rooms { get; set; } = new List<Room>();
        public string Name { get; set; }
        public int Pin { get; set; }
        public int Max { get; set; }
        public List<Player> InPlayer { get; set; }
        public Card LastCard { get; set; }
    }
}
