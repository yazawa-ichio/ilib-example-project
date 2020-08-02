using System;
using System.Collections;
using System.Collections.Generic;
using ILib.MVVM;

namespace App.MVVM
{

	public partial class UISetupVM : ViewModelBase
	{

		/// BindingPath : Single
		/// Target: ILib.MVVM.ButtonBind
		public bool SingleValue
		{
			get { return GetImpl<bool>("Single"); }
			set { SetImpl<bool>("Single", value); }
		}

		/// BindingPath : Single
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnSingle
		{
			add
			{
				m_Event.Subscribe("Single", value);
			}
			remove
			{
				m_Event.Unsubscribe("Single", value);
			}
		}

		/// BindingPath : LocalHost
		/// Target: ILib.MVVM.ButtonBind
		public bool LocalHostValue
		{
			get { return GetImpl<bool>("LocalHost"); }
			set { SetImpl<bool>("LocalHost", value); }
		}

		/// BindingPath : LocalHost
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnLocalHost
		{
			add
			{
				m_Event.Subscribe("LocalHost", value);
			}
			remove
			{
				m_Event.Unsubscribe("LocalHost", value);
			}
		}

		/// BindingPath : LocalGest
		/// Target: ILib.MVVM.ButtonBind
		public bool LocalGestValue
		{
			get { return GetImpl<bool>("LocalGest"); }
			set { SetImpl<bool>("LocalGest", value); }
		}

		/// BindingPath : LocalGest
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnLocalGest
		{
			add
			{
				m_Event.Subscribe("LocalGest", value);
			}
			remove
			{
				m_Event.Unsubscribe("LocalGest", value);
			}
		}



	}

}