using UnityEngine;

namespace App.InGame
{
	public class GoalObject : MonoBehaviour, IStageObject
	{
		[SerializeField]
		Role m_Target = default;

		public void Setup(Role role, bool isHost)
		{
			if (TryGetComponent<Collider>(out var target))
			{
				//target.enabled = isHost;
			}
		}

		void OnTriggerEnter(Collider collision)
		{
			if (collision.gameObject.TryGetComponent<BallObject>(out var ball))
			{
				ball.Goal(m_Target);
			}
		}

	}
}