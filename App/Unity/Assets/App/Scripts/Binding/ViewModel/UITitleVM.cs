using System;
using UVMBinding;

namespace App.Binding
{
	public class UITitleVM : ViewModel
	{
		[Event]
		public Action OnStart { get; set; }
	}
}