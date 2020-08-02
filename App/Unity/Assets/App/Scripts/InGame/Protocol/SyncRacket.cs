using UnityEngine;

namespace App.InGame
{
	public class SyncRacket
	{
		public Role Role;

		public Vector3 Position;

		public Vector3 Velocity;

		public SyncRacket(Role role, Vector3 position, Vector3 velocity)
		{
			Role = role;
			Position = position;
			Velocity = velocity;
		}
	}
}