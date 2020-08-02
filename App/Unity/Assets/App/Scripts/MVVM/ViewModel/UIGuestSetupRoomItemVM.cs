using System;
using System.Collections;
using System.Collections.Generic;
using ILib.MVVM;

namespace App.MVVM
{

	public partial class UIGuestSetupRoomItemVM : ViewModelBase
	{

		/// BindingPath : Button
		/// Target: ILib.MVVM.ButtonBind
		public bool ButtonValue
		{
			get { return GetImpl<bool>("Button"); }
			set { SetImpl<bool>("Button", value); }
		}

		/// BindingPath : Button
		/// Sender: ILib.MVVM.ButtonBind
		public event Action<int> OnButton
		{
			add
			{
				m_Event.Subscribe<int>("Button", value);
			}
			remove
			{
				m_Event.Unsubscribe<int>("Button", value);
			}
		}

		/// BindingPath : Name
		/// Target: ILib.MVVM.TextBind
		public string Name
		{
			get { return GetImpl<string>("Name"); }
			set { SetImpl<string>("Name", value); }
		}



	}

}