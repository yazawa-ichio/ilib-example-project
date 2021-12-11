using UnityEngine.UI;
using UVMBinding;

namespace App.Binding
{
	public class SelectableInteractableBind : Binder<bool>
	{
		Selectable m_Target;
		protected override void OnBind()
		{
			TryGetComponent(out m_Target);
		}

		protected override void UpdateValue(bool val)
		{
			m_Target.interactable = val;
		}
	}
}