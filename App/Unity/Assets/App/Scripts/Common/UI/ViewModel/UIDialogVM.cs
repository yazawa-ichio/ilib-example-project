using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UIDialogVM : ViewModel
	{
		[Bind]
		public string MessageText { get; set; }

		[Bind]
		public string TitleText { get; set; }

		[Bind]
		public string ButtonYesText { get; set; } = "YES";

		[Bind]
		public string ButtonNoText { get; set; } = "NO";

		[Bind]
		public string ButtonOkText { get; set; } = "OK";

		[Event]
		public Action OnButtonYes { get; set; }

		[Event]
		public Action OnButonNo { get; set; }

		[Event]
		public Action OnButtonOk { get; set; }

		[Bind]
		public bool ActiveConfirm { get; set; }

		[Bind]
		public bool ActiveNofify { get; set; }

		[Bind]
		public bool ButtonMaskValue { get; set; }

		[Event]
		public Action OnButtonMask { get; set; }

	}

}