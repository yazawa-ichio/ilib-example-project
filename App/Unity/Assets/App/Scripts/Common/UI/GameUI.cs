using ILib.ServInject;
using UnityEngine;

namespace App
{

	public class GameUI : ServiceMonoBehaviour<IGameUI>, IGameUI
	{
		[SerializeField]
		AppUIStack m_UIStack = null;

		public AppUIStack UIStack => m_UIStack;

	}

}