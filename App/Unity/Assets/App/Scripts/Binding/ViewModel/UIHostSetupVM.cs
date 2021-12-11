using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UIHostSetupVM : ViewModel
	{
		[Bind]
		public bool DecisionEnabled { get; set; }

		[Event]
		public Action<string> OnDecision { get; set; }

		[Bind]
		public string Message { get; set; }

		[Event]
		public Action OnBack { get; set; }

		[Bind]
		public string RoomName { get; set; }

		[Event]
		public Action<string> OnRoomName { get; set; }
	}

}