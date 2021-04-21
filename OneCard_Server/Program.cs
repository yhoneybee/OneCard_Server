using System;
using System.Collections.Generic;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Room
    {
        public Room(HostID owner, string name, int pw, int max_player)
        {
            this.owner = owner;
            this.name = name;
            this.pw = pw;
            this.max_player = max_player;
        }
        public bool Join(HostID ID)
        {
            if (hostIDs.Count < max_player)
            {
                hostIDs.Add(ID);
                return true;
            }
            return false;
        }
        public bool Leave(HostID ID)
        {
            for (int i = 0; i < hostIDs.Count; i++)
            {
                if (hostIDs[i] == ID)
                {
                    hostIDs.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
        public HostID owner;
        public string name;
        public int pw;
        public int max_player;
        public FastArray<HostID> hostIDs = new FastArray<HostID>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            NetServer server = new NetServer();
        }
    }
}
