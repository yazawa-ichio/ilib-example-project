using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UIGuestSetupRoomItemVM : ViewModel
	{
		[Bind]
		public string Name { get; set; }

		[Event]
		public Action<int> OnButton { get; set; }
	}

}