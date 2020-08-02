using UnityEngine;

namespace App.InGame
{

	public class BallObject : MonoBehaviour, IStageObject
	{

		public event System.Action<Role> OnGoal;

		public event System.Action<SyncBall> SendSyncBall;

		bool m_IsHost;
		Rigidbody m_Rigidbody;
		float m_CountDown;

		public Vector3 Position => m_Rigidbody.position;

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Rigidbody.isKinematic = true;
		}

		public void Setup(Role role, bool isHost)
		{
			m_IsHost = true;
			transform.localPosition = Vector3.zero;
		}

		internal void Stop()
		{
			m_Rigidbody.isKinematic = true;
		}

		public void Play()
		{
			m_Rigidbody.isKinematic = false;
			if (m_IsHost)
			{
				m_Rigidbody.velocity = new Vector3(0, 0, -5f);
			}
			Sync();
		}

		public void SyncBall(SyncBall msg)
		{
			m_Rigidbody.isKinematic = false;
			m_Rigidbody.MovePosition(msg.Position);
			m_Rigidbody.velocity = msg.Velocity;
		}

		void Sync()
		{
			if (!m_Rigidbody.isKinematic)
			{
				m_CountDown = Config.I.BallSyncInterval;
				SendSyncBall?.Invoke(new SyncBall(m_Rigidbody.position, m_Rigidbody.velocity));
			}
		}

		void FixedUpdate()
		{
			m_CountDown--;
			if (m_CountDown < 0)
			{
				Sync();
			}
		}

		void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Racket"))
			{
				float x = (m_Rigidbody.position.x - collision.transform.position.x) / 2;
				float z = collision.relativeVelocity.z > 0 ? 1 : -1;
				Vector3 dev = new Vector3(x, 0, z).normalized;
				m_Rigidbody.velocity = dev * Config.I.BallSpeed;
			}

			if (m_IsHost)
			{
				Sync();
			}


		}

		public void Goal(Role role)
		{
			if (m_Rigidbody.isKinematic) return;

			m_Rigidbody.velocity = Vector3.zero;

			m_Rigidbody.isKinematic = true;

			if (!m_IsHost) return;

			OnGoal?.Invoke(role);

		}

	}
}