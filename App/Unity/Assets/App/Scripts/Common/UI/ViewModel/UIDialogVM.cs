using ILib.MVVM;
using System;

namespace App.MVVM
{

	public partial class UIDialogVM : ViewModelBase
	{

		/// BindingPath : ButtonYesText
		/// Target: ILib.MVVM.TextBind
		public string ButtonYesText
		{
			get { return GetImpl<string>("ButtonYesText"); }
			set { SetImpl<string>("ButtonYesText", value); }
		}

		/// BindingPath : ButtonNoText
		/// Target: ILib.MVVM.TextBind
		public string ButtonNoText
		{
			get { return GetImpl<string>("ButtonNoText"); }
			set { SetImpl<string>("ButtonNoText", value); }
		}

		/// BindingPath : ButtonOkText
		/// Target: ILib.MVVM.TextBind
		public string ButtonOkText
		{
			get { return GetImpl<string>("ButtonOkText"); }
			set { SetImpl<string>("ButtonOkText", value); }
		}

		/// BindingPath : ButtonYes
		/// Target: ILib.MVVM.ButtonBind
		public bool ButtonYesValue
		{
			get { return GetImpl<bool>("ButtonYes"); }
			set { SetImpl<bool>("ButtonYes", value); }
		}

		/// BindingPath : ButtonYes
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnButtonYes
		{
			add
			{
				Event.Subscribe("ButtonYes", value);
			}
			remove
			{
				Event.Unsubscribe("ButtonYes", value);
			}
		}

		/// BindingPath : ButonNo
		/// Target: ILib.MVVM.ButtonBind
		public bool ButonNoValue
		{
			get { return GetImpl<bool>("ButonNo"); }
			set { SetImpl<bool>("ButonNo", value); }
		}

		/// BindingPath : ButonNo
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnButonNo
		{
			add
			{
				Event.Subscribe("ButonNo", value);
			}
			remove
			{
				Event.Unsubscribe("ButonNo", value);
			}
		}

		/// BindingPath : ButtonOk
		/// Target: ILib.MVVM.ButtonBind
		public bool ButtonOkValue
		{
			get { return GetImpl<bool>("ButtonOk"); }
			set { SetImpl<bool>("ButtonOk", value); }
		}

		/// BindingPath : ButtonOk
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnButtonOk
		{
			add
			{
				Event.Subscribe("ButtonOk", value);
			}
			remove
			{
				Event.Unsubscribe("ButtonOk", value);
			}
		}

		/// BindingPath : MessageText
		/// Target: ILib.MVVM.TextBind
		public string MessageText
		{
			get { return GetImpl<string>("MessageText"); }
			set { SetImpl<string>("MessageText", value); }
		}

		/// BindingPath : ActiveConfirm
		/// Target: ILib.MVVM.ActiveBind
		public bool ActiveConfirm
		{
			get { return GetImpl<bool>("ActiveConfirm"); }
			set { SetImpl<bool>("ActiveConfirm", value); }
		}

		/// BindingPath : ActiveNofify
		/// Target: ILib.MVVM.ActiveBind
		public bool ActiveNofify
		{
			get { return GetImpl<bool>("ActiveNofify"); }
			set { SetImpl<bool>("ActiveNofify", value); }
		}

		/// BindingPath : ButtonMask
		/// Target: ILib.MVVM.ButtonBind
		public bool ButtonMaskValue
		{
			get { return GetImpl<bool>("ButtonMask"); }
			set { SetImpl<bool>("ButtonMask", value); }
		}

		/// BindingPath : ButtonMask
		/// Sender: ILib.MVVM.ButtonBind
		public event Action OnButtonMask
		{
			add
			{
				Event.Subscribe("ButtonMask", value);
			}
			remove
			{
				Event.Unsubscribe("ButtonMask", value);
			}
		}

		/// BindingPath : TitleText
		/// Target: ILib.MVVM.TextBind
		public string TitleText
		{
			get { return GetImpl<string>("TitleText"); }
			set { SetImpl<string>("TitleText", value); }
		}



	}

}