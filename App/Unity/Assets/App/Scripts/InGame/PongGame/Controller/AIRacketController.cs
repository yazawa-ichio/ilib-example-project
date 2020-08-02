using UnityEngine;

namespace App.InGame
{
	public class AIRacketController : IRacketController
	{
		Racket m_Racket;
		Ball m_Ball;

		public void Setup(Racket racket, Ball ball)
		{
			m_Racket = racket;
			m_Ball = ball;
		}

		public void FixedUpdate()
		{
			var h = m_Ball.Position.x - m_Racket.Position.x;
			h = h * 100 * Time.fixedDeltaTime;
			m_Racket.Velocity = new Vector3(h, 0, 0);
		}

	}
}