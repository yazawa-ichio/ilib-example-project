using ILib.ServInject;
using UnityEngine;

namespace App.InGame
{
	public interface IPongGame
	{
		void Install(IPongGameController controller);
	}

	public class PongGame : ServiceMonoBehaviour<IPongGame>, IPongGame
	{
		[SerializeField]
		Camera m_Camera = default;
		[SerializeField]
		RacketObject m_RacketObject1 = default;
		[SerializeField]
		RacketObject m_RacketObject2 = default;
		[SerializeField]
		BallObject m_BallObject = default;

		IPongGameController m_Controller;

		public void Install(IPongGameController controller)
		{
			m_Controller = controller;
			foreach (var obj in GetComponentsInChildren<IStageObject>(true))
			{
				obj.Setup(m_Controller.Role, m_Controller.IsHost);
			}
			if (m_Controller.Role == Role.Player2)
			{
				var angle = m_Camera.transform.localEulerAngles;
				angle.y = 180;
				m_Camera.transform.localEulerAngles = angle;
			}

			var ball = new Ball(m_BallObject);
			var racket1 = new Racket(m_RacketObject1, Role.Player1);
			var racket2 = new Racket(m_RacketObject2, Role.Player2);

			m_Controller.Setup(ball, racket1, racket2);
		}

		void FixedUpdate()
		{
			if (m_Controller != null)
			{
				m_Controller.FixedUpdate();
			}
		}

	}
}