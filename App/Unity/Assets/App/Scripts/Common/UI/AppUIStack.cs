using ILib.MVVM;
using ILib.ServInject;
using ILib.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
	public class AppUIStack : UIStack<IViewModel, IControl>
	{
		[SerializeField]
		GraphicRaycaster m_Raycaster = null;

		IResourceLoader Loader => ServInjector.Resolve<IResourceLoader>();
		ISystemUI SystemUI => ServInjector.Resolve<ISystemUI>();
		RefCountToggle m_RaycasterToggle;
		System.IDisposable m_BlockUI;

		void Awake()
		{
			m_RaycasterToggle = new RefCountToggle();
			m_RaycasterToggle.OnChange = (x) => m_Raycaster.enabled = !x;
		}

		void OnDestroy()
		{
			m_BlockUI?.Dispose();
			m_BlockUI = null;
		}

		protected override async Task<GameObject> Load<T>(string path, IViewModel prm)
		{
			return await Loader.Load<GameObject>("UI/" + path);
		}

		protected override void OnStartProcess()
		{
			m_RaycasterToggle.AddRef();
			m_BlockUI = m_BlockUI ?? SystemUI.BlockUI();
		}

		protected override void OnEndProcess()
		{
			m_RaycasterToggle.RemoveRef();
			m_BlockUI?.Dispose();
			m_BlockUI = null;
		}

		public IStackEntry Push<T>(string path, System.Action<T> action) where T : IViewModel, new()
		{
			T vm = new T();
			action(vm);
			return Push(path, vm);
		}

		public IStackEntry Switch<T>(string path, System.Action<T> action) where T : IViewModel, new()
		{
			T vm = new T();
			action(vm);
			return Switch(path, vm);
		}
	}

}