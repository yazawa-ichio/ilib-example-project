using ILib.MVVM;
using UnityEngine.UI;

namespace App.MVVM
{
	public class SelectableInteractableBind : LightBind<bool, Selectable>
	{
		protected override void UpdateValue(bool val)
		{
			m_Target.interactable = val;
		}
	}
}