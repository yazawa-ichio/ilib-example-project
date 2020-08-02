using ILib.ServInject;
using UnityEngine;

namespace App.UI
{
	public class CanvasHelper : MonoBehaviour, IInject<IUIEssential>
	{
		public enum Mode
		{
			Default,
			System,
			World,
		}

		[SerializeField]
		Canvas m_Canvas = null;
		[SerializeField]
		Mode m_Mode = Mode.Default;

		IUIEssential m_UIEssential = null;

		void IInject<IUIEssential>.Install(IUIEssential service)
		{
			m_UIEssential = service;
		}

		void Awake()
		{
			ServInjector.Inject(this);
			if (m_UIEssential == null) return;
			switch (m_Mode)
			{
				case Mode.Default:
					m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
					m_Canvas.worldCamera = m_UIEssential.UICamera;
					break;
				case Mode.System:
					m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
					m_Canvas.worldCamera = m_UIEssential.SystemUICamera;
					break;
				case Mode.World:
					m_Canvas.renderMode = RenderMode.WorldSpace;
					break;
			}
		}

	}
}