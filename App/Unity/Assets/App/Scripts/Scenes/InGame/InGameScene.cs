using App.InGame;
using App.InGame.Protocol;
using App.Net;
using Cysharp.Threading.Tasks;
using ILib.Caller;
using ILib.ServInject;

namespace App
{
	public enum InGameMode
	{
		Single,
		Host,
		Guest,
	}

	public class InGameParam
	{
		public InGameMode Mode { get; set; }
	}

	public class InGameScene : GameScene<InGameParam>
	{

		public override string GetUnitySceneName()
		{
			return "InGame";
		}

		[Inject]
		public IPongGame PongGame { get; set; }

		IPongGameController m_Controller = null;

		protected override void OnCompleteRun()
		{
			m_Controller = CreateController();
			m_Controller.Broadcast += Broadcast;
			m_Controller.SendTarget += SendTarget;
			PongGame.Install(m_Controller);
		}

		IPongGameController CreateController()
		{
			switch (Param.Mode)
			{
				case InGameMode.Host:
					return new HostPongGameController();
				case InGameMode.Guest:
					return new GuestPongGameController();
			}
			return new SinglePongGameController();
		}

		protected override void OnPreShutdown()
		{
			Realtime.Cleanup();
		}

		void Broadcast(object obj)
		{
			var buf = Processor.Pack(obj);
			Realtime.Send(buf);
			OnReceive(buf);
		}

		void SendTarget(object obj)
		{
			Realtime.Send(Processor.Pack(obj));
		}

		[Handle(RealtimeEvent.Disconnect)]
		void OnDisconnect()
		{
			SystemUI.Alart("切断されました").Forget();
		}

		[Handle(RealtimeEvent.Receive)]
		void OnReceive(byte[] data)
		{
			var obj = Processor.Unpack(data);
			if (obj is Result result)
			{
				OnResult(result);
			}
			m_Controller?.OnReceive(obj);
		}

		async void OnResult(Result result)
		{
			try
			{
				Realtime.Cleanup();
				string message = "負け";
				if (m_Controller.Role == result.Winner)
				{
					message = "勝ち";
				}
				await SystemUI.Notify(message);
				await Switch<BootScene>();
			}
			catch
			{
				SystemUI.Alart().Forget();
			}
		}

	}

}