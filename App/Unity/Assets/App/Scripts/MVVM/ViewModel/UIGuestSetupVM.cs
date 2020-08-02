using System;
using System.Collections;
using System.Collections.Generic;
using ILib.MVVM;

namespace App.MVVM
{

	public partial class UIGuestSetupVM : ViewModelBase
	{

		/// BindingPath : Rooms
		/// Target: Content (ILib.MVVM.CollectionBind)
		public ILib.MVVM.IViewModel[] Rooms
		{
			get { return GetImpl<ILib.MVVM.IViewModel[]>("Rooms"); }
			set { SetImpl<ILib.MVVM.IViewModel[]>("Rooms", value); }
		}

		/// BindingPath : Update
		/// Target: ILib.MVVM.ButtonBind
		public bool UpdateValue
		{
			get { return GetImpl<bool>("Update"); }
			set { SetImpl<bool>("Update", value); }
		}

		/// BindingPath : Update
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnUpdate
		{
			add
			{
				m_Event.Subscribe("Update", value);
			}
			remove
			{
				m_Event.Unsubscribe("Update", value);
			}
		}

		/// BindingPath : Message
		/// Target: ILib.MVVM.TextBind
		public string Message
		{
			get { return GetImpl<string>("Message"); }
			set { SetImpl<string>("Message", value); }
		}

		/// BindingPath : Back
		/// Target: ILib.MVVM.ButtonBind
		public bool BackValue
		{
			get { return GetImpl<bool>("Back"); }
			set { SetImpl<bool>("Back", value); }
		}

		/// BindingPath : Back
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnBack
		{
			add
			{
				m_Event.Subscribe("Back", value);
			}
			remove
			{
				m_Event.Unsubscribe("Back", value);
			}
		}



	}

}