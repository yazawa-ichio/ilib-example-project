using UnityEngine;

namespace App.InGame
{
	public class Ball
	{
		BallObject m_Obj;
		public Ball(BallObject obj) => m_Obj = obj;

		public Vector3 Position => m_Obj.Position;

		public event System.Action<Role> OnGoal
		{
			add => m_Obj.OnGoal += value;
			remove => m_Obj.OnGoal -= value;
		}

		public event System.Action<SyncBall> SendSyncBall
		{
			add => m_Obj.SendSyncBall += value;
			remove => m_Obj.SendSyncBall -= value;
		}

		public void Play()
		{
			m_Obj.Play();
		}

		public void Stop()
		{
			m_Obj.Stop();
		}

		public void SyncBall(SyncBall msg)
		{
			m_Obj.SyncBall(msg);
		}

	}
}