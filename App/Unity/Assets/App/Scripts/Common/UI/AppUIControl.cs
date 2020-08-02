using ILib.MVVM;
using ILib.UI;
using System.Threading.Tasks;
using UnityEngine;

namespace App.UI
{
	public class AppUIControl : UIControl<IViewModel>, IExecuteBack
	{
		[EventKey]
		public enum Event
		{
			Back,
			TryBack,
		}

		protected IView m_View;
		protected IViewModel m_Context;

		[SerializeField]
		bool m_CanBack = true;

		protected sealed override Task OnCreated(IViewModel prm)
		{
			m_Context = prm;
			m_View = GetComponent<IView>();
			m_View.Attach(prm);
			return OnCreatedImpl();
		}

		protected virtual Task OnCreatedImpl()
		{
			m_Context.Event.Subscribe(Event.Back, Close);
			m_Context.Event.Subscribe(Event.TryBack, () => TryBack());
			return Util.Successed;
		}

		bool IExecuteBack.TryBack()
		{
			return TryBack();
		}

		protected bool TryBack()
		{
			if (CanBack())
			{
				Close();
				return true;
			}
			return false;
		}

		protected virtual bool CanBack()
		{
			return m_CanBack;
		}

	}
}