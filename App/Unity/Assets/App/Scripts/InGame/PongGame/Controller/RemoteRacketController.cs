namespace App.InGame
{
	public class RemoteRacketController : IRacketController
	{
		Racket m_Racket;

		public void FixedUpdate()
		{
		}

		public void Setup(Racket racket, Ball ball)
		{
			m_Racket = racket;
		}

		public void SyncRacket(SyncRacket data)
		{
			if (m_Racket.Role == data.Role)
			{
				m_Racket.Position = data.Position;
				m_Racket.Velocity = data.Velocity;
			}
		}
	}

}