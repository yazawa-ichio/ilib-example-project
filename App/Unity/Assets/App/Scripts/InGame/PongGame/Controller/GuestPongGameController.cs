using App.InGame.Protocol;
using System;

namespace App.InGame
{
	public class GuestPongGameController : IPongGameController
	{
		public bool IsHost => false;

		public Role Role => Role.Player2;

		public event Action<object> SendTarget;

		public event Action<object> Broadcast { add { } remove { } }

		Ball m_Ball;
		RemoteRacketController m_Player1 = new RemoteRacketController();
		RacketController m_Player2 = new RacketController();

		public void FixedUpdate()
		{
			m_Player1?.FixedUpdate();
			m_Player2?.FixedUpdate();
		}

		public void OnReceive(object obj)
		{
			if (obj is Ready ready)
			{
				if (ready.Reply) return;
				ready.Reply = true;
				SendTarget?.Invoke(ready);
			}
			else if (obj is SyncRacket syncRacket)
			{
				m_Player1.SyncRacket(syncRacket);
			}
			else if (obj is SyncBall syncBall)
			{
				m_Ball.SyncBall(syncBall);
			}
		}

		public void Setup(Ball ball, Racket racket1, Racket racket2)
		{
			m_Ball = ball;
			m_Player1.Setup(racket1, ball);
			m_Player2.Setup(racket2, ball);
			m_Player2.SendSyncRacket += SyncRacket;
			SendTarget?.Invoke(new Ready());
		}

		void SyncRacket(SyncRacket data)
		{
			SendTarget?.Invoke(data);
		}
	}
}