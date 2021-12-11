using ILib.UI;
using System.Threading.Tasks;
using UnityEngine;
using UVMBinding;

namespace App.UI
{
	public class AppUIControl : UIControl<ViewModel>, IExecuteBack
	{
		//[EventKey]
		public enum Event
		{
			Back,
			TryBack,
		}

		protected View m_View;
		protected ViewModel m_Context;

		[SerializeField]
		bool m_CanBack = true;

		protected sealed override Task OnCreated(ViewModel prm)
		{
			m_Context = prm;
			m_View = GetComponent<View>();
			m_View.Attach(prm);
			return OnCreatedImpl();
		}

		protected virtual Task OnCreatedImpl()
		{
			m_Context.Event.Subscribe("OnBack", Close);
			m_Context.Event.Subscribe(Event.TryBack.ToString(), () => TryBack());
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