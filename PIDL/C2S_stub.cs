﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace C2S
{
    public class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool CreateRoomDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name, int pin, int max);  
		public CreateRoomDelegate CreateRoom = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name, int pin, int max)
		{ 
			return false;
		};
		public delegate bool JoinRoomDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name, int pin);  
		public JoinRoomDelegate JoinRoom = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name, int pin)
		{ 
			return false;
		};
		public delegate bool LeaveRoomDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name);  
		public LeaveRoomDelegate LeaveRoom = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string name)
		{ 
			return false;
		};
		public delegate bool ReadyDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public ReadyDelegate Ready = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool UnReadyDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public UnReadyDelegate UnReady = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool DownDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num);  
		public DownDelegate Down = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num)
		{ 
			return false;
		};
		public delegate bool DrawDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int count);  
		public DrawDelegate Draw = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int count)
		{ 
			return false;
		};
		public delegate bool ChangeSymbolDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol);  
		public ChangeSymbolDelegate ChangeSymbol = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol)
		{ 
			return false;
		};
		public delegate bool OneCardDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public OneCardDelegate OneCard = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool ZeroCardDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public ZeroCardDelegate ZeroCard = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.CreateRoom:
            ProcessReceivedMessage_CreateRoom(__msg, pa, hostTag, remote);
            break;
        case Common.JoinRoom:
            ProcessReceivedMessage_JoinRoom(__msg, pa, hostTag, remote);
            break;
        case Common.LeaveRoom:
            ProcessReceivedMessage_LeaveRoom(__msg, pa, hostTag, remote);
            break;
        case Common.Ready:
            ProcessReceivedMessage_Ready(__msg, pa, hostTag, remote);
            break;
        case Common.UnReady:
            ProcessReceivedMessage_UnReady(__msg, pa, hostTag, remote);
            break;
        case Common.Down:
            ProcessReceivedMessage_Down(__msg, pa, hostTag, remote);
            break;
        case Common.Draw:
            ProcessReceivedMessage_Draw(__msg, pa, hostTag, remote);
            break;
        case Common.ChangeSymbol:
            ProcessReceivedMessage_ChangeSymbol(__msg, pa, hostTag, remote);
            break;
        case Common.OneCard:
            ProcessReceivedMessage_OneCard(__msg, pa, hostTag, remote);
            break;
        case Common.ZeroCard:
            ProcessReceivedMessage_ZeroCard(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_CreateRoom(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        string name; Nettention.Proud.Marshaler.Read(__msg,out name);	
int pin; Nettention.Proud.Marshaler.Read(__msg,out pin);	
int max; Nettention.Proud.Marshaler.Read(__msg,out max);	
core.PostCheckReadMessage(__msg, RmiName_CreateRoom);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=name.ToString()+",";
parameterString+=pin.ToString()+",";
parameterString+=max.ToString()+",";
        NotifyCallFromStub(Common.CreateRoom, RmiName_CreateRoom,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.CreateRoom;
        summary.rmiName = RmiName_CreateRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =CreateRoom (remote,ctx , name, pin, max );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_CreateRoom);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.CreateRoom;
        summary.rmiName = RmiName_CreateRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoinRoom(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        string name; Nettention.Proud.Marshaler.Read(__msg,out name);	
int pin; Nettention.Proud.Marshaler.Read(__msg,out pin);	
core.PostCheckReadMessage(__msg, RmiName_JoinRoom);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=name.ToString()+",";
parameterString+=pin.ToString()+",";
        NotifyCallFromStub(Common.JoinRoom, RmiName_JoinRoom,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoinRoom;
        summary.rmiName = RmiName_JoinRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoinRoom (remote,ctx , name, pin );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoinRoom);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoinRoom;
        summary.rmiName = RmiName_JoinRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_LeaveRoom(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        string name; Nettention.Proud.Marshaler.Read(__msg,out name);	
core.PostCheckReadMessage(__msg, RmiName_LeaveRoom);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=name.ToString()+",";
        NotifyCallFromStub(Common.LeaveRoom, RmiName_LeaveRoom,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.LeaveRoom;
        summary.rmiName = RmiName_LeaveRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =LeaveRoom (remote,ctx , name );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_LeaveRoom);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.LeaveRoom;
        summary.rmiName = RmiName_LeaveRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Ready(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_Ready);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.Ready, RmiName_Ready,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Ready;
        summary.rmiName = RmiName_Ready;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Ready (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Ready);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Ready;
        summary.rmiName = RmiName_Ready;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_UnReady(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_UnReady);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.UnReady, RmiName_UnReady,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.UnReady;
        summary.rmiName = RmiName_UnReady;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =UnReady (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_UnReady);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.UnReady;
        summary.rmiName = RmiName_UnReady;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Down(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int symbol; Nettention.Proud.Marshaler.Read(__msg,out symbol);	
int num; Nettention.Proud.Marshaler.Read(__msg,out num);	
core.PostCheckReadMessage(__msg, RmiName_Down);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=symbol.ToString()+",";
parameterString+=num.ToString()+",";
        NotifyCallFromStub(Common.Down, RmiName_Down,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Down;
        summary.rmiName = RmiName_Down;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Down (remote,ctx , symbol, num );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Down);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Down;
        summary.rmiName = RmiName_Down;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Draw(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int count; Nettention.Proud.Marshaler.Read(__msg,out count);	
core.PostCheckReadMessage(__msg, RmiName_Draw);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=count.ToString()+",";
        NotifyCallFromStub(Common.Draw, RmiName_Draw,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Draw;
        summary.rmiName = RmiName_Draw;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Draw (remote,ctx , count );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Draw);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Draw;
        summary.rmiName = RmiName_Draw;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_ChangeSymbol(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int symbol; Nettention.Proud.Marshaler.Read(__msg,out symbol);	
core.PostCheckReadMessage(__msg, RmiName_ChangeSymbol);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=symbol.ToString()+",";
        NotifyCallFromStub(Common.ChangeSymbol, RmiName_ChangeSymbol,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.ChangeSymbol;
        summary.rmiName = RmiName_ChangeSymbol;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =ChangeSymbol (remote,ctx , symbol );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_ChangeSymbol);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.ChangeSymbol;
        summary.rmiName = RmiName_ChangeSymbol;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_OneCard(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_OneCard);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.OneCard, RmiName_OneCard,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.OneCard;
        summary.rmiName = RmiName_OneCard;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =OneCard (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_OneCard);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.OneCard;
        summary.rmiName = RmiName_OneCard;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_ZeroCard(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_ZeroCard);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.ZeroCard, RmiName_ZeroCard,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.ZeroCard;
        summary.rmiName = RmiName_ZeroCard;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =ZeroCard (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_ZeroCard);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.ZeroCard;
        summary.rmiName = RmiName_ZeroCard;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
		#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_CreateRoom="CreateRoom";
public const string RmiName_JoinRoom="JoinRoom";
public const string RmiName_LeaveRoom="LeaveRoom";
public const string RmiName_Ready="Ready";
public const string RmiName_UnReady="UnReady";
public const string RmiName_Down="Down";
public const string RmiName_Draw="Draw";
public const string RmiName_ChangeSymbol="ChangeSymbol";
public const string RmiName_OneCard="OneCard";
public const string RmiName_ZeroCard="ZeroCard";
       
public const string RmiName_First = RmiName_CreateRoom;
		#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_CreateRoom="";
public const string RmiName_JoinRoom="";
public const string RmiName_LeaveRoom="";
public const string RmiName_Ready="";
public const string RmiName_UnReady="";
public const string RmiName_Down="";
public const string RmiName_Draw="";
public const string RmiName_ChangeSymbol="";
public const string RmiName_OneCard="";
public const string RmiName_ZeroCard="";
       
public const string RmiName_First = "";
		#endif

		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

