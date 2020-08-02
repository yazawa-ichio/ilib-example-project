using System;
using System.Collections;
using System.Collections.Generic;
using ILib.MVVM;

namespace App.MVVM
{

	public partial class UIHostSetupVM : ViewModelBase
	{

		/// BindingPath : Decision
		/// Target: ILib.MVVM.ButtonBind
		public bool DecisionValue
		{
			get { return GetImpl<bool>("Decision"); }
			set { SetImpl<bool>("Decision", value); }
		}

		/// BindingPath : Decision
		/// Sender: ILib.MVVM.ButtonBind
		public event Action<string> OnDecision
		{
			add
			{
				m_Event.Subscribe<string>("Decision", value);
			}
			remove
			{
				m_Event.Unsubscribe<string>("Decision", value);
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

		/// BindingPath : RoomName
		/// Target: ILib.MVVM.InputFieldBind
		public string RoomNameValue
		{
			get { return GetImpl<string>("RoomName"); }
			set { SetImpl<string>("RoomName", value); }
		}

		/// BindingPath : RoomName
		/// Sender: ILib.MVVM.InputFieldBind
		public event Action<string> OnRoomName
		{
			add
			{
				m_Event.Subscribe<string>("RoomName", value);
			}
			remove
			{
				m_Event.Unsubscribe<string>("RoomName", value);
			}
		}



	}

}