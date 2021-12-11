using App.InGame;
using ILib.Caller;
using ILib.Debugs;
using ILib.ServInject;
using UnityEngine;

namespace App.Debugs
{

	public class DebugManager : ServiceMonoBehaviour<IDebugManager>, IDebugManager
	{
		[SerializeField]
		ScDebugMenu m_DebugMenu = null;

		System.IDisposable m_Lock;

#if DEBUG_BUILD
		void Start()
		{
			m_DebugMenu.Contexts.Bind(this);
			m_DebugMenu.Contexts.Bind(Config.I);
		}

		void Update()
		{
#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(1))
			{
				Open();
			}
#endif
			if (Input.touchCount > 3)
			{
				Open();
			}

			if (!m_DebugMenu.enabled && m_Lock != null)
			{
				m_Lock.Dispose();
				m_Lock = null;
			}
		}

		public void Open()
		{
			m_DebugMenu.Open();
			m_Lock?.Dispose();
			m_Lock = ServInjector.Resolve<ISystemUI>().BlockUI();
		}

		public void Close()
		{
			m_DebugMenu.Close();
			m_Lock?.Dispose();
			m_Lock = null;
		}

		public void ReOpen()
		{
			m_DebugMenu.enabled = false;
			m_DebugMenu.Open();
			m_Lock?.Dispose();
			m_Lock = ServInjector.Resolve<ISystemUI>().BlockUI();
		}

		public void Bind<T>(T obj) where T : class
		{
			m_DebugMenu.Contexts.Bind(obj);
		}

		public void Unbind<T>(T obj) where T : class
		{
			m_DebugMenu.Contexts.Unbind(obj);
		}
#endif

	}

#if DEBUG_BUILD
	public class RebootDebugButton : ButtonContent<DebugManager>
	{
		protected override string Label => "再起動";

		protected override void OnClick()
		{
			ServInjector.Resolve<IDispatcher>().Broadcast(GameManager.Event.Reboot);
		}
	}
#endif

}