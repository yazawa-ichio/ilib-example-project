using UnityEngine;

namespace App.InGame
{
	public class SyncBall
	{
		public Vector3 Position;

		public Vector3 Velocity;

		public SyncBall(Vector3 position, Vector3 velocity)
		{
			Position = position;
			Velocity = velocity;
		}
	}
}