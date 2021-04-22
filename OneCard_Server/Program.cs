using System;
using System.Collections.Generic;
using Nettention.Proud;

namespace OneCard_Server
{
    public class Room
    {
        public Room(HostID group_ID, string name, int pw, int max_player)
        {
            this.group_ID = group_ID;
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
        public bool GameStart()
        {
            if (isStart)
                return false;
            isStart = true;
            return true;
        }

        public HostID group_ID;
        public string name;
        public int pw;
        public int max_player;
        public FastArray<HostID> hostIDs = new FastArray<HostID>();
        public bool isStart = false;
    }
    class Program
    {
        public static List<Room> rooms = new List<Room>();
        public static NetServer server = new NetServer();
        public static S2C.Proxy proxy = new S2C.Proxy();
        public static C2S.Stub stub = new C2S.Stub();
        public static List<HostID> clients = new List<HostID>();
        static void Main(string[] _)
        {
            StartServerParameter param = new StartServerParameter();
            param.tcpPorts.Add(6475);
            param.udpPorts.Add(6475);
            param.protocolVersion = new Nettention.Proud.Guid("{E54C4938-8BFC-4443-87F3-386C1AA388F0}");
            param.m_enableAutoConnectionRecoveryOnServer = true;

            stub.RoomCreate = OnRoomCreate;
            stub.JoinRoom = OnJoinRoom;
            stub.LeaveRoom = OnLeaveRoom;
            stub.GameStart = OnGameStart;

            server.ClientJoinHandler = OnJoinServer;
            server.ClientLeaveHandler = OnLeaveServer;

            server.AttachProxy(proxy);
            server.AttachStub(stub);
            server.Start(param);

            Print();

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine($"접속자 수 : {clients.Count}");
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
                }
            }
        }

        public static void Print()
        {
            int port = 6475;
            //string ip = "(wifi : 172.30.1.22, hamachi : 25.75.45.185, localhost : 127.0.0.1)";
            string ip = "(wifi : 192.168.1.254, hamachi : NONE, localhost : 127.0.0.1)";

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

        private static bool OnRoomCreate(HostID remote, RmiContext rmiContext, string room_name, int pw, int max_player)
        {
            foreach (var room in rooms)
                if (room.name == room_name)
                {
                    proxy.Create_Fail(remote, RmiContext.ReliableSend);
                    return false;
                }

            HostID[] ids = new HostID[1];
            ids[0] = remote;

            rooms.Add(new Room(server.CreateP2PGroup(ids, new ByteArray()), room_name, pw, max_player));

            proxy.Create_OK(remote, RmiContext.ReliableSend);

            return true;
        }

        private static bool OnJoinRoom(HostID remote, RmiContext rmiContext, string room_name, int pw)
        {
            foreach (var room in rooms)
            {
                if (room.name == room_name)
                {
                    if (!room.isStart)
                    {
                        if (server.JoinP2PGroup(remote, room.group_ID))
                        {
                            if (room.Join(remote))
                            {
                                proxy.Join_OK(remote, RmiContext.ReliableSend);
                                return true;
                            }
                        }
                    }
                }
            }
            proxy.Join_Fail(remote, RmiContext.ReliableSend);
            return false;
        }

        private static bool OnLeaveRoom(HostID remote, RmiContext rmiContext, string room_name)
        {
            foreach (var room in rooms)
            {
                if (room.name == room_name)
                {
                    if (server.LeaveP2PGroup(remote, room.group_ID))
                    {
                        if (room.Leave(remote))
                        {
                            proxy.Leave_OK(remote, RmiContext.ReliableSend);
                            return true;
                        }
                    }
                }
            }
            proxy.Leave_Fail(remote, RmiContext.ReliableSend);
            return false;
        }

        private static bool OnGameStart(HostID remote, RmiContext rmiContext, string room_name)
        {
            foreach (var room in rooms)
            {
                if (room.name == room_name)
                {
                    if (room.GameStart())
                    {
                        proxy.Start_OK(room.group_ID, RmiContext.ReliableSend);
                        return true;
                    }
                }
            }

            proxy.Start_Fail(remote, RmiContext.ReliableSend);
            return false;
        }
    }
}
