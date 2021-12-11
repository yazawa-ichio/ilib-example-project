using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UIGuestSetupVM : ViewModel
	{
		[Bind]
		public Collection<UIGuestSetupRoomItemVM> Rooms { get; set; }

		[Bind]
		public bool UpdateEnabled { get; set; }

		[Event]
		public Action OnUpdate { get; set; }

		[Bind]
		public string Message { get; set; }

		[Event]
		public Action OnBack { get; set; }
	}

}