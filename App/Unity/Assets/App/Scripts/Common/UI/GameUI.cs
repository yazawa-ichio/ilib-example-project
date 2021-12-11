using ILib.ServInject;
using UnityEngine;
using UVMBinding;

namespace App
{

	public class GameUI : ServiceMonoBehaviour<IGameUI>, IGameUI
	{
		[SerializeField]
		View m_Main;
		[SerializeField]
		AppUIStack m_UIStack = null;

		public ViewModel MainVM
		{
			set
			{
				if (m_Main)
				{
					m_Main.ViewModel = value;
				}
			}
		}

		public AppUIStack UIStack => m_UIStack;

	}

}