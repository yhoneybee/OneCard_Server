using System;
using System.Collections.Generic;
using Nettention.Proud;
using System.Linq;

namespace OneCard_Server
{
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
                            Console.Write($"[ Name {room.Name}, Pin {room.Pin}, Max {room.Max} ] ");
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
            return true;
        }

        private static bool OnOneCard(HostID remote, RmiContext rmiContext)
        {
            return true;
        }

        private static bool OnChangeSymbol(HostID remote, RmiContext rmiContext, int symbol)
        {
            Player player = Player.Players.Find((p) => { return p.ID == remote; });
            player.InRoom.ChangeSymbol(symbol);
            return true;
        }

        private static bool OnDraw(HostID remote, RmiContext rmiContext, int count)
        {
            return true;
        }

        private static bool OnDown(HostID remote, RmiContext rmiContext, int symbol, int num)
        {
            return true;
        }

        private static bool OnUnReady(HostID remote, RmiContext rmiContext)
        {
            Player player = Player.Players.Find((p) => { return p.ID == remote; });
            player.ReadySwitch(false);
            return true;
        }

        private static bool OnReady(HostID remote, RmiContext rmiContext)
        {
            Player player = Player.Players.Find((p) => { return p.ID == remote; });
            player.ReadySwitch(true);
            return true;
        }

        private static bool OnLeaveRoom(HostID remote, RmiContext rmiContext, string name)
        {
            Player player = Player.Players.Find((p) => { return p.ID == remote; });
            Room room = Room.Rooms.Find((r) => { return r.Name == name; });
            if (room.Leave(player))
            {
                return true;
            }
            return false;
        }

        private static bool OnJoinRoom(HostID remote, RmiContext rmiContext, string name, int pin)
        {
            Player player = Player.Players.Find((p) => { return p.ID == remote; });
            Room room = Room.Rooms.Find((r) => { return r.Name == name; });
            if (room.Pin == pin)
            {
                room.Join(player);
                return true;
            }
            return false;
        }

        private static bool OnCreateRoom(HostID remote, RmiContext rmiContext, string name, int pin, int max)
        {
            if (Room.Create(name, pin, max))
            {
                Room.Rooms.Last().Join(Player.Players.Find((p) => { return p.ID == remote; }));
                return true;
            }
            else
            {
                return false;
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
            Player l = Player.Players.Find((p) => { return p.ID == clientInfo.hostID; });
            l.InRoom.Leave(l);
            Player.Players.Remove(l);
            Console.WriteLine($"{clientInfo.hostID} leave to server...");
        }

        private static void OnJoinServer(NetClientInfo clientInfo)
        {
            Player j = new Player(clientInfo.hostID);
            Player.Players.Add(j);
            Console.WriteLine($"{clientInfo.hostID} join to server!");
        }
    }
}
