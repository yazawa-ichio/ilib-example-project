using System;
using UnityEngine;

namespace App.InGame
{

	public class RacketController : IRacketController
	{
		Ball m_Ball;
		Racket m_Racket;
		bool m_PrevVelocityPlus;
		bool m_PrevVelocityLarge;
		float m_CountDown;

		public event Action<SyncRacket> SendSyncRacket;

		public void Sync(SyncRacket data) { }

		public void Setup(Racket racket, Ball ball)
		{
			m_Ball = ball;
			m_Racket = racket;
		}

		public void FixedUpdate()
		{
			var axis = Input.GetAxisRaw("Horizontal");
			var h = axis;
			if (m_Racket.Role == Role.Player2)
			{
				h = -h;
			}
			h = h * Time.fixedDeltaTime * Config.I.RacketMovePower;

			m_Racket.Velocity = new Vector3(h, 0, 0);

			var plus = h >= 0;
			var large = Mathf.Abs(axis) > 0.7f;
			if (m_PrevVelocityPlus != plus || m_PrevVelocityLarge != large)
			{
				Sync();
			}
			m_PrevVelocityLarge = large;
			m_PrevVelocityPlus = plus;
			m_CountDown--;
			if (m_CountDown < 0)
			{
				Sync();
			}
		}

		void Sync()
		{
			m_CountDown = Config.I.RacketSyncInterval;
			SendSyncRacket?.Invoke(new SyncRacket(m_Racket.Role, m_Racket.Position, m_Racket.Velocity));
		}

	}

}