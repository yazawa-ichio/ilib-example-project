using ILib.ServInject;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App
{
	using UI;

	public class UIManager : ServiceMonoBehaviour<IUIEssential>, IUIEssential
	{
		[SerializeField]
		Camera m_Camera = null;
		public Camera UICamera => m_Camera;
		[SerializeField]
		Camera m_SystemCamera = null;
		public Camera SystemUICamera => m_SystemCamera;

		[SerializeField]
		EventSystem m_EventSystem = null;
		[SerializeField]
		SystemUIQueue m_SystemQueue = null;
		[SerializeField]
		GameObject m_InputBlock = null;
		[SerializeField]
		GameObject m_Connect = null;

		SystemUI m_SystemUI;

		protected override void OnAwake()
		{
			m_Connect.SetActive(false);
			m_SystemUI = new SystemUI(m_SystemQueue, m_InputBlock, m_Connect);
			ServInjector.Bind<ISystemUI>(m_SystemUI);
		}

		protected override void OnDestroyEvent()
		{
			ServInjector.Unbind<ISystemUI>(m_SystemUI);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (TryBack())
				{
					ServInjector.Resolve<ISound>()?.Play(SoundID.UI.Select);
				}
			}
		}

		bool TryBack()
		{
			if (m_SystemUI.HasProcess())
			{
				return false;
			}
			if (m_SystemUI.ExecuteBack())
			{
				return true;
			}
			var gameUI = ServInjector.Resolve<IGameUI>();
			if (gameUI != null)
			{
				if (gameUI.UIStack.HasProcess)
				{
					return false;
				}
				if (gameUI.ExecuteBack())
				{
					return true;
				}
			}
			return false;
		}

		public void EventLock()
		{
			m_EventSystem.enabled = false;
		}

		public void EventUnLock()
		{
			m_EventSystem.enabled = true;
		}

	}
}