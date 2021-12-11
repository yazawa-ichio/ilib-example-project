using System;
using UVMBinding;

namespace App.Binding
{

	public partial class UISetupVM : ViewModel
	{
		[Event]
		public Action OnSingle { get; set; }

		[Event]
		public Action OnLocalHost { get; set; }

		[Event]
		public Action OnLocalGest { get; set; }

		[Event]
		public Action OnBack { get; set; }
	}

}