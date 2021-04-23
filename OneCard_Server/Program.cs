using System;
using System.Collections.Generic;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Card
    {
        public Card(bool is_black, Symbols symbol, int num)
        {
            IsBlack = is_black;
            Symbol = symbol;
            Num = num;
        }

        public enum Symbols { SPADE, DIAMOND, CLOVER, HEART, }
        public bool IsBlack { get; set; }
        public Symbols Symbol { get; set; }
        public int Num { get; set; }
    }
    public class Player
    {
        public static List<Player> Players = new List<Player>();

        public Player(HostID id, bool is_ready)
        {
            ID = id;
            IsReady = is_ready;
            Accessed = null;
            Hands = new List<Card>();
            Players.Add(this);
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

        public void Draw(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Random rd = new Random();
                Hands.Add(new Card(rd.Next(0, 1) == 0, (Card.Symbols)rd.Next(0, 3), rd.Next(1, 13)));
            }
        }

        public HostID ID { get; set; }
        public Room Accessed { get; set; }
        public bool IsReady { get; set; }
        public List<Card> Hands { get; set; }
    }
    public class Room
    {
        public static List<Room> Rooms = new List<Room>();

        public Room()
        {
            Rooms.Add(this);
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

        public string Name { get; set; }
        public int Pw { get; set; }
        public HostID GroupID { get; set; }
        public int MaxPlayer { get; set; }
        public bool IsStart { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
    class Program
    {
        public static NetServer server = new NetServer();
        /*        public static S2C.Proxy proxy = new S2C.Proxy();
                public static C2S.Stub stub = new C2S.Stub();*/

        static void Main(string[] _)
        {
            StartServerParameter param = new StartServerParameter();
            param.tcpPorts.Add(6475);
            param.udpPorts.Add(6475);
            param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
            param.m_enableAutoConnectionRecoveryOnServer = true;

            server.ClientJoinHandler = OnJoinServer;
            server.ClientLeaveHandler = OnLeaveServer;

            /*            server.AttachProxy(proxy);
                        server.AttachStub(stub);*/
            server.Start(param);

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
                        //Console.WriteLine($"방 개수 : {rooms.Count}");
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
                        server.Stop();
                        server.Start(param);
                        Print();
                        break;
                    case "q":
                    case "Q":
                        Console.WriteLine("서버 구동 종료...");
                        server.Stop();
                        Console.Clear();
                        return;
                    default:
                        Print();
                        break;
                }
            }
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
            Player.Players.Remove(Player.Find(clientInfo.hostID));
        }

        private static void OnJoinServer(NetClientInfo clientInfo)
        {
            Console.WriteLine($"[ Joined Server, ID : {clientInfo.hostID} ]");
            Console.WriteLine();
            Player.Players.Add(new Player(clientInfo.hostID, false));
        }
    }
}
