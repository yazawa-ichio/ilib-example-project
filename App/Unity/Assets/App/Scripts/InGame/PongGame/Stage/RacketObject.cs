using UnityEngine;

namespace App.InGame
{

	public class RacketObject : MonoBehaviour, IStageObject
	{

		public Rigidbody Rigidbody { get; private set; }

		public void Setup(Role role, bool isHost)
		{
			if (TryGetComponent<Collider>(out var target))
			{
				//target.enabled = isHost;
			}
			transform.localPosition = Vector3.zero;
			Rigidbody = GetComponent<Rigidbody>();
		}

	}

}