using App.InGame.Protocol;
using System;

namespace App.InGame
{
	public class HostPongGameController : IPongGameController
	{
		public bool IsHost => true;

		public Role Role => Role.Player1;

		public event Action<object> SendTarget;

		public event Action<object> Broadcast;

		Ball m_Ball;
		bool m_Ready;
		RacketController m_Player1 = new RacketController();
		RemoteRacketController m_Player2 = new RemoteRacketController();

		public void FixedUpdate()
		{
			m_Player1?.FixedUpdate();
			m_Player2?.FixedUpdate();
		}

		public void OnReceive(object obj)
		{
			if (obj is Ready ready)
			{
				if (m_Ready) return;
				m_Ready = true;
				m_Ball.Play();
			}
			else if (obj is SyncRacket syncRacket)
			{
				m_Player2.SyncRacket(syncRacket);
			}
		}

		public void Setup(Ball ball, Racket racket1, Racket racket2)
		{
			m_Ready = false;
			m_Ball = ball;
			m_Ball.OnGoal += OnGoal;
			m_Ball.SendSyncBall += SyncBall;
			m_Player1.Setup(racket1, ball);
			m_Player1.SendSyncRacket += SyncRacket;
			m_Player2.Setup(racket2, ball);
			SendTarget?.Invoke(new Ready());
		}

		void SyncRacket(SyncRacket data)
		{
			SendTarget?.Invoke(data);
		}

		void OnGoal(Role role)
		{
			Broadcast?.Invoke(new Result
			{
				Winner = role,
			});
		}

		void SyncBall(SyncBall data)
		{
			SendTarget?.Invoke(data);
		}

	}
}