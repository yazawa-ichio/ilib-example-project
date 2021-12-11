using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UIInputDialogVM : ViewModel
	{

		[Bind]
		public string ButtonOkText { get; set; }

		[Event]
		public Action<string> OnButtonOk { get; set; }

		[Event]
		public Action OnButtonMask { get; set; }

		[Bind]
		public string TitleText { get; set; }

		[Bind]
		public string InputTextValue { get; set; }

		[Event]
		public Action<string> OnInputText { get; set; }

	}

}