using UnityEngine;

namespace App.InGame
{
	public class Racket
	{
		public Vector3 Velocity
		{
			get => m_Obj.Rigidbody.velocity;
			set => m_Obj.Rigidbody.velocity = value;
		}

		public Vector3 Position
		{
			get => m_Obj.Rigidbody.position;
			set => m_Obj.Rigidbody.position = value;
		}

		public Role Role { get; private set; }

		RacketObject m_Obj;

		public Racket(RacketObject obj, Role role)
		{
			m_Obj = obj;
			Role = role;
		}

	}
}