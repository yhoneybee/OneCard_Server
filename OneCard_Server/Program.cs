using System;
using System.Collections.Generic;
using Nettention.Proud;
using System.Linq;

namespace OneCard_Server
{
    public class Card
    {
        public Card(bool is_black, int symbol, int num)
        {
            IsBlack = is_black;
            Symbol = symbol;
            Num = num;
        }

        public bool IsBlack { get; set; }
        public int Symbol { get; set; }
        public int Num { get; set; }
    }
    public class Player
    {
        public static List<Player> Players { get; set; } = new List<Player>();

        public Player(HostID id)
        {
            ID = id;
            IsReady = false;
            Joined = null;
            Hands = new List<Card>();
        }

        public static Player Find(HostID ID)
        {
            foreach (var player in Players)
            {
                if (player.ID == ID)
                    return player;
            }
            return null;
        }

        public Card FindCard(int symbol, int num)
        {
            return Hands.Find((card) => { return card.Symbol == symbol && card.Num == num; });
        }

        public void Draw()
        {
            for (int i = 0; i < Joined.CardStack; i++)
            {
                Random rd = new Random();
                Hands.Add(new Card(rd.Next(0, 1) == 0, rd.Next(0, 3), rd.Next(1, 15)));
            }
            Joined.CardStack = 1;
        }

        public void Attack(int count)
        {
            Joined.CardStack += count;
        }

        public void Invalid()
        {
            Joined.CardStack = 1;
        }

        public void ChangeSymbol(int symbol)
        {
            Joined.LastCard.Symbol = symbol;
        }

        public void Down(Card card)
        {
            if (Joined.LastCard.Symbol == card.Symbol ||
                Joined.LastCard.Num == card.Num)
            {
                switch (card.Num)
                {
                    case 1:
                        Attack(3);
                        Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        break;
                    case 2:
                        Attack(2);
                        Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        break;
                    case 4:
                        Invalid();
                        Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        break;
                    case 11:
                        Joined.NextTurn(Room.NextTurnCase.JUMP);
                        break;
                    case 12:
                        Joined.Reverse();
                        Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        break;
                    case 13:
                        Joined.NextTurn(Room.NextTurnCase.AGAIN);
                        break;
                    case 14: // black
                        if (Joined.LastCard.IsBlack == true)
                        {
                            Attack(5);
                            Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        }
                        break;
                    case 15: // color
                        if (Joined.LastCard.IsBlack == false)
                        {
                            Attack(7);
                            Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        }
                        break;
                    default:
                        Joined.NextTurn(Room.NextTurnCase.NORMAL);
                        break;
                }
                Hands.Remove(card);
                Joined.LastCard = card;
            }
        }

        public HostID ID { get; set; }
        public Room Joined { get; set; }
        public bool IsReady { get; set; }
        public List<Card> Hands { get; set; }
    }
    public class Room
    {
        public static List<Room> Rooms = new List<Room>();

        public Room(HostID group_id, string name, int pw, int max)
        {
            GroupID = group_id;
            Name = name;
            Pw = pw;
            MaxPlayer = max;
            IsStart = false;
            TurnIndex = new Random().Next(0, MaxPlayer - 1);
            NextValue = 1;
            Rank = 0;
        }

        public static Room Find(string name)
        {
            foreach (var room in Rooms)
            {
                if (room.Name == name)
                    return room;
            }
            return null;
        }

        public bool Join(Player player)
        {
            if (Players.Count < MaxPlayer)
            {
                Players.Add(player);
                player.Joined = this;
                Console.WriteLine($"[  OK  ] {player.ID} join to {Name} Room");
                return true;
            }
            else
            {
                Console.WriteLine($"[ FAIL ] {player.ID} join to {Name} Room");
                return false;
            }
        }

        public void Leave(Player player)
        {
            Players.Remove(player);
            Console.WriteLine($"[  OK  ] {player.ID} leave to {Name} Room");
        }

        public bool AllReady()
        {
            Console.WriteLine($"Count : {Players.Count}");
            if (Players.Count <= 1)
                return false;
            foreach (var p in Players)
                if (!p.IsReady)
                {
                    Console.WriteLine($"{p.ID} is Not Ready...");
                    return false;
                }
            return true;
        }

        public Player NowTurn() => Players[TurnIndex];

        public int NextTurn(NextTurnCase turn_case)
        {
            if (IsStart)
            {
                int next = 0;
                switch (turn_case)
                {
                    case NextTurnCase.NORMAL:
                        next = 1;
                        break;
                    case NextTurnCase.JUMP:
                        next = 2;
                        break;
                }
                for (int i = 0; i < next; i++)
                {
                    TurnIndex += NextValue;
                    if (TurnIndex == MaxPlayer)
                        TurnIndex = 0;
                    if (TurnIndex < 0)
                        TurnIndex = MaxPlayer - 1;
                }
                Program.Proxy.TurnStart(Players[TurnIndex].ID, RmiContext.ReliableSend);
                return TurnIndex;
            }
            return -1;
        }

        public void Reverse()
        {
            switch (NextValue)
            {
                case 1:
                    NextValue = -1;
                    break;
                case -1:
                    NextValue = 1;
                    break;
            }
        }

        public Player OneCard()
        {
            foreach (var p in Players)
            {
                if (p.Hands.Count == 1)
                    return p;
            }
            return null;
        }

        public string Name { get; set; }
        public int Pw { get; set; }
        public HostID GroupID { get; set; }
        public int MaxPlayer { get; set; }
        public bool IsStart { get; set; }
        public enum NextTurnCase { NORMAL, JUMP, AGAIN, }
        public int TurnIndex { get; set; }
        int NextValue { get; set; }
        public Card LastCard { get; set; }
        public int CardStack { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int Rank { get; set; }
    }
    class Program
    {
        public static NetServer Server { get; set; } = new NetServer();
        public static S2C.Proxy Proxy { get; set; } = new S2C.Proxy();
        public static C2S.Stub Stub { get; set; } = new C2S.Stub();

        static void Main(string[] _)
        {
            StartServerParameter param = new StartServerParameter();
            param.tcpPorts.Add(6475);
            param.udpPorts.Add(6475);
            param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");

            Server.ClientJoinHandler = OnJoinServer;
            Server.ClientLeaveHandler = OnLeaveServer;

            Stub.CreateRoom = OnCreateRoom;
            Stub.JoinRoom = OnJoinRoom;
            Stub.LeaveRoom = OnLeaveRoom;
            Stub.Ready = OnReady;
            Stub.UnReady = OnUnReady;

            Stub.Down = OnDown;
            Stub.Draw = OnDraw;
            Stub.Attack = OnAttack;
            Stub.Invalid = OnInvalid;
            Stub.ChangeSymbol = OnChangeSymbol;
            Stub.OneCard = OnOneCard;
            Stub.ZeroCard = OnZeroCard;

            Server.AttachProxy(Proxy);
            Server.AttachStub(Stub);
            Server.Start(param);

            Print();

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine($"접속자 수 : {Player.Players.Count}");
                        Console.Write("player IDs : ");
                        foreach (var player in Player.Players)
                        {
                            Console.Write($"[ {player.ID} ] ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ReadLine();
                        Print();
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine($"방 개수 : {Room.Rooms.Count}");
                        Console.Write("Room Infos : ");
                        foreach (var room in Room.Rooms)
                        {
                            Console.Write($"[ Name {room.Name}, Pin {room.Pw}, Max {room.MaxPlayer} ] ");
                        }
                        Console.WriteLine();
                        Console.ReadLine();
                        Print();
                        break;
                    case "":
                        Print();
                        break;
                    case "r":
                    case "R":
                        Console.Clear();
                        Console.WriteLine("서버 재 구동중...");
                        Player.Players.Clear();
                        Room.Rooms.Clear();
                        Server.Stop();
                        Server.Start(param);
                        Print();
                        break;
                    case "q":
                    case "Q":
                        Console.WriteLine("서버 구동 종료...");
                        Server.Stop();
                        Console.Clear();
                        return;
                    default:
                        Print();
                        break;
                }
            }
        }

        private static bool OnZeroCard(HostID remote, RmiContext rmiContext)
        {
            Player end = Player.Find(remote);
            Proxy.ExcludeGame(remote, rmiContext);
            Proxy.Rank(remote, rmiContext, ++end.Joined.Rank);
            end.Joined.Leave(end);
            return true;
        }

        private static bool OnOneCard(HostID remote, RmiContext rmiContext)
        {
            Player one_card = Player.Find(remote).Joined.OneCard();
            if (one_card.ID == remote)
            {
                Console.WriteLine($"{remote} is OneCard!");
            }
            else
            {
                one_card.Draw();
            }
            return true;
        }

        private static bool OnChangeSymbol(HostID remote, RmiContext rmiContext, int symbol)
        {
            Player.Find(remote).ChangeSymbol(symbol);
            return true;
        }

        private static bool OnInvalid(HostID remote, RmiContext rmiContext)
        {
            Player.Find(remote).Invalid();
            return true;
        }

        private static bool OnAttack(HostID remote, RmiContext rmiContext, int count)
        {
            Player.Find(remote).Attack(count);
            return true;
        }

        private static bool OnDraw(HostID remote, RmiContext rmiContext, int count)
        {
            Player.Find(remote).Draw();
            return true;
        }

        private static bool OnDown(HostID remote, RmiContext rmiContext, int symbol, int num)
        {
            Player.Find(remote).Down(Player.Find(remote).FindCard(symbol, num));
            return true;
        }

        private static bool OnUnReady(HostID remote, RmiContext rmiContext)
        {
            Console.WriteLine($"{remote} has UnReady");
            Player.Find(remote).IsReady = false;
            return true;
        }

        private static bool OnReady(HostID remote, RmiContext rmiContext)
        {
            Console.WriteLine($"{remote} has Ready");
            Player p = Player.Find(remote);
            p.IsReady = true;
            if (p.Joined.AllReady())
            {
                foreach (var player in p.Joined.Players)
                {
                    Console.WriteLine($"ID : {player.ID}");
                    Proxy.Start(player.ID, rmiContext);
                }
            }
            return true;
        }

        private static bool OnLeaveRoom(HostID remote, RmiContext rmiContext, string name)
        {
            Room l = Room.Find(name);
            if (l != null)
            {
                l.Players.Remove(Player.Find(remote));
                Server.LeaveP2PGroup(l.GroupID, remote);
                Console.WriteLine($"{remote} was leave to {name}");
                if (l.Players.Count == 0)
                {
                    Room.Rooms.Remove(l);
                    Console.WriteLine($"Room {name} was Destory");
                }
            }
            return false;
        }

        private static bool OnJoinRoom(HostID remote, RmiContext rmiContext, string name, int pin)
        {
            Room j = Room.Find(name);
            if (j != null)
            {
                if (j.Pw == pin)
                {
                    j.Join(Player.Find(remote));
                    Server.JoinP2PGroup(j.GroupID, remote);
                    Console.WriteLine($"{remote} was join to {name}");
                    return true;
                }
            }
            return false;
        }

        private static bool OnCreateRoom(HostID remote, RmiContext rmiContext, string name, int pin, int max)
        {
            HostID[] ids = { remote };
            HostID g = Server.CreateP2PGroup(ids, new ByteArray());
            Room.Rooms.Add(new Room(g, name, pin, max));
            Console.WriteLine($"{remote} was Create to {name}");
            return true;
        }

        public static void Print()
        {
            int port = 6475;
            string ip = "(wifi : 172.30.1.22, hamachi : 25.75.45.185, localhost : 127.0.0.1)";
            //string ip = "(wifi : 192.168.1.254, hamachi : NONE, localhost : 127.0.0.1)";

            Console.Clear();
            Console.WriteLine($"서버 구동 시작!");
            Console.WriteLine($"ip : {ip}");
            Console.WriteLine($"port(tcp / udp) : {port}");
            Console.WriteLine();
            Console.WriteLine("[ binding key ]");
            Console.WriteLine("1 : 접속자 정보 보기");
            Console.WriteLine("2 : 방 정보 보기");
            Console.WriteLine("R : 서버 다시 시작");
            Console.WriteLine("Q : 나가기");
            Console.WriteLine();
        }

        private static void OnLeaveServer(NetClientInfo clientInfo, ErrorInfo errorinfo, ByteArray comment)
        {
            Console.WriteLine($"[ Leaved Server, ID : {clientInfo.hostID} ]");
            Console.WriteLine();
            Player del = Player.Find(clientInfo.hostID);
            OnLeaveRoom(clientInfo.hostID, RmiContext.ReliableSend, del.Joined.Name);
            Player.Players.Remove(del);
        }

        private static void OnJoinServer(NetClientInfo clientInfo)
        {
            Console.WriteLine($"[ Joined Server, ID : {clientInfo.hostID} ]");
            Player.Players.Add(new Player(clientInfo.hostID));
            Console.WriteLine($"Total : {Player.Players.Count()}");
            Console.WriteLine();
        }
    }
}
