using App.InGame.Protocol;
using System;

namespace App.InGame
{
	public class SinglePongGameController : IPongGameController
	{
		public bool IsHost => true;

		public Role Role => Role.Player1;

		public event Action<object> SendTarget { add { } remove { } }

		public event Action<object> Broadcast;

		RacketController m_Player1 = new RacketController();
		AIRacketController m_Player2 = new AIRacketController();

		public void Setup(Ball ball, Racket racket1, Racket racket2)
		{
			m_Player1.Setup(racket1, ball);
			m_Player2.Setup(racket2, ball);
			ball.Play();
			ball.OnGoal += OnGoal;
		}

		public void FixedUpdate()
		{
			m_Player1.FixedUpdate();
			m_Player2.FixedUpdate();
		}

		void OnGoal(Role role)
		{
			Broadcast?.Invoke(new Result
			{
				Winner = role,
			});
		}

		public void OnReceive(object obj) { }

	}
}