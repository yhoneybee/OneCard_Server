




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace S2C
{
	public class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool StartDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public StartDelegate Start = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool TurnStartDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public TurnStartDelegate TurnStart = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool LastCardDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num);  
		public LastCardDelegate LastCard = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num)
		{ 
			return false;
		};
		public delegate bool DownDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num);  
		public DownDelegate Down = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num)
		{ 
			return false;
		};
		public delegate bool DrawDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num);  
		public DrawDelegate Draw = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol, int num)
		{ 
			return false;
		};
		public delegate bool ChangeSymbolDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol);  
		public ChangeSymbolDelegate ChangeSymbol = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int symbol)
		{ 
			return false;
		};
		public delegate bool RankDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int rank);  
		public RankDelegate Rank = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int rank)
		{ 
			return false;
		};
		public delegate bool ExcludeGameDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public ExcludeGameDelegate ExcludeGame = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool NowCardsCountDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, Nettention.Proud.HostID client, int count);  
		public NowCardsCountDelegate NowCardsCount = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, Nettention.Proud.HostID client, int count)
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
        case Common.Start:
            ProcessReceivedMessage_Start(__msg, pa, hostTag, remote);
            break;
        case Common.TurnStart:
            ProcessReceivedMessage_TurnStart(__msg, pa, hostTag, remote);
            break;
        case Common.LastCard:
            ProcessReceivedMessage_LastCard(__msg, pa, hostTag, remote);
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
        case Common.Rank:
            ProcessReceivedMessage_Rank(__msg, pa, hostTag, remote);
            break;
        case Common.ExcludeGame:
            ProcessReceivedMessage_ExcludeGame(__msg, pa, hostTag, remote);
            break;
        case Common.NowCardsCount:
            ProcessReceivedMessage_NowCardsCount(__msg, pa, hostTag, remote);
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
    void ProcessReceivedMessage_Start(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_Start);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.Start, RmiName_Start,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Start;
        summary.rmiName = RmiName_Start;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Start (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Start);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Start;
        summary.rmiName = RmiName_Start;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_TurnStart(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_TurnStart);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.TurnStart, RmiName_TurnStart,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.TurnStart;
        summary.rmiName = RmiName_TurnStart;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =TurnStart (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_TurnStart);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.TurnStart;
        summary.rmiName = RmiName_TurnStart;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_LastCard(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int symbol; Nettention.Proud.Marshaler.Read(__msg,out symbol);	
int num; Nettention.Proud.Marshaler.Read(__msg,out num);	
core.PostCheckReadMessage(__msg, RmiName_LastCard);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=symbol.ToString()+",";
parameterString+=num.ToString()+",";
        NotifyCallFromStub(Common.LastCard, RmiName_LastCard,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.LastCard;
        summary.rmiName = RmiName_LastCard;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =LastCard (remote,ctx , symbol, num );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_LastCard);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.LastCard;
        summary.rmiName = RmiName_LastCard;
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

        int symbol; Nettention.Proud.Marshaler.Read(__msg,out symbol);	
int num; Nettention.Proud.Marshaler.Read(__msg,out num);	
core.PostCheckReadMessage(__msg, RmiName_Draw);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=symbol.ToString()+",";
parameterString+=num.ToString()+",";
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
        bool __ret =Draw (remote,ctx , symbol, num );

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
    void ProcessReceivedMessage_Rank(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int rank; Nettention.Proud.Marshaler.Read(__msg,out rank);	
core.PostCheckReadMessage(__msg, RmiName_Rank);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=rank.ToString()+",";
        NotifyCallFromStub(Common.Rank, RmiName_Rank,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Rank;
        summary.rmiName = RmiName_Rank;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Rank (remote,ctx , rank );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Rank);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Rank;
        summary.rmiName = RmiName_Rank;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_ExcludeGame(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_ExcludeGame);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.ExcludeGame, RmiName_ExcludeGame,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.ExcludeGame;
        summary.rmiName = RmiName_ExcludeGame;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =ExcludeGame (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_ExcludeGame);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.ExcludeGame;
        summary.rmiName = RmiName_ExcludeGame;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NowCardsCount(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        Nettention.Proud.HostID client; Nettention.Proud.Marshaler.Read(__msg,out client);	
int count; Nettention.Proud.Marshaler.Read(__msg,out count);	
core.PostCheckReadMessage(__msg, RmiName_NowCardsCount);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=client.ToString()+",";
parameterString+=count.ToString()+",";
        NotifyCallFromStub(Common.NowCardsCount, RmiName_NowCardsCount,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NowCardsCount;
        summary.rmiName = RmiName_NowCardsCount;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NowCardsCount (remote,ctx , client, count );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NowCardsCount);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NowCardsCount;
        summary.rmiName = RmiName_NowCardsCount;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
		#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_Start="Start";
public const string RmiName_TurnStart="TurnStart";
public const string RmiName_LastCard="LastCard";
public const string RmiName_Down="Down";
public const string RmiName_Draw="Draw";
public const string RmiName_ChangeSymbol="ChangeSymbol";
public const string RmiName_Rank="Rank";
public const string RmiName_ExcludeGame="ExcludeGame";
public const string RmiName_NowCardsCount="NowCardsCount";
       
public const string RmiName_First = RmiName_Start;
		#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_Start="";
public const string RmiName_TurnStart="";
public const string RmiName_LastCard="";
public const string RmiName_Down="";
public const string RmiName_Draw="";
public const string RmiName_ChangeSymbol="";
public const string RmiName_Rank="";
public const string RmiName_ExcludeGame="";
public const string RmiName_NowCardsCount="";
       
public const string RmiName_First = "";
		#endif

		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

