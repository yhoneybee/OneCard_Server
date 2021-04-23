using System;
using System.Collections.Generic;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Player
    {
        public Player(HostID ID, bool isReady, bool isExclude, int cards)
        {
            this.ID = ID;
            room_name = default;
            this.isReady = isReady;
            this.isExclude = isExclude;
            this.cards = cards;
        }

        public HostID ID;
        public string room_name;
        public bool isReady;
        public bool isExclude;
        public int cards;
    }
    public class Room
    {
        public Room(HostID group_ID, string name, int pw, int max_player)
        {
            this.group_ID = group_ID;
            Name = name;
            Pw = pw;
            this.max_player = max_player;
        }

        public bool Join()
        {


            return false;
        }

        public bool GameStart()
        {
            if (isStart)
                return false;
            foreach (var p in players.data)
                if (!p.isReady)
                    return false;
            isStart = true;
            return true;
        }

        public HostID group_ID;
        public string Name { get; }
        public int Pw { get; }
        public int max_player;
        public bool isStart = false;
        public FastArray<Player> players = new FastArray<Player>();
    }
    class Program
    {
        public static List<Room> rooms = new List<Room>();
        public static NetServer server = new NetServer();
/*        public static S2C.Proxy proxy = new S2C.Proxy();
        public static C2S.Stub stub = new C2S.Stub();*/
        public static List<HostID> clients = new List<HostID>();
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
                        Console.WriteLine($"접속자 수 : {clients.Count}");
                        Console.Write("Client IDs : ");
                        foreach (var c in clients)
                        {
                            Console.Write($"[ {c} ] ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ReadLine();
                        Print();
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine($"방 개수 : {rooms.Count}");
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
                        clients.Clear();
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
            clients.Remove(clientInfo.hostID);
        }

        private static void OnJoinServer(NetClientInfo clientInfo)
        {
            Console.WriteLine($"[ Joined Server, ID : {clientInfo.hostID} ]");
            Console.WriteLine();
            clients.Add(clientInfo.hostID);
        }
    }
}
