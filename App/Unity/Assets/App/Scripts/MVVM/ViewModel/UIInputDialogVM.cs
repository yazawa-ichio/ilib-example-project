using ILib.MVVM;
using System;

namespace App.MVVM
{

	public partial class UIInputDialogVM : ViewModelBase
	{

		/// BindingPath : ButtonOkText
		/// Target: ILib.MVVM.TextBind
		public string ButtonOkText
		{
			get { return GetImpl<string>("ButtonOkText"); }
			set { SetImpl<string>("ButtonOkText", value); }
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
		public event Action<string> OnButtonOk
		{
			add
			{
				Event.Subscribe<string>("ButtonOk", value);
			}
			remove
			{
				Event.Unsubscribe<string>("ButtonOk", value);
			}
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

		/// BindingPath : InputText
		/// Target: ILib.MVVM.InputFieldBind
		public string InputTextValue
		{
			get { return GetImpl<string>("InputText"); }
			set { SetImpl<string>("InputText", value); }
		}

		/// BindingPath : InputText
		/// Sender: ILib.MVVM.InputFieldBind
		public event Action<string> OnInputText
		{
			add
			{
				Event.Subscribe<string>("InputText", value);
			}
			remove
			{
				Event.Unsubscribe<string>("InputText", value);
			}
		}



	}

}