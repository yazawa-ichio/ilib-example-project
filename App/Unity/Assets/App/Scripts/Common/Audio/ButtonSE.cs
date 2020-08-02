using ILib.ServInject;
using UnityEngine;
using UnityEngine.UI;

namespace App.Audio
{

	public class ButtonSE : MonoBehaviour
	{
		public enum Type
		{
			None,
			Select,
		}

		[SerializeField]
		Type m_Type = Type.Select;

		private void Awake()
		{
			var button = GetComponent<Button>();
			Debug.Assert(button != null);
			button?.onClick.AddListener(OnClick);
		}

		void OnClick()
		{
			var sound = ServInjector.Resolve<ISound>();
			sound?.Play(GetId());
		}

		SoundID.UI GetId()
		{
			switch (m_Type)
			{
				case Type.Select: return SoundID.UI.Select;
			}
			return SoundID.UI.None;
		}


	}

}