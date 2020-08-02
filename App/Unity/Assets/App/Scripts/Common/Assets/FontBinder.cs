using UnityEngine;
using UnityEngine.UI;

namespace App.Assets
{
	public class FontBinder : AssetBinder<Font>
	{
		protected override void OnLoad(Font asset)
		{
			if (TryGetComponent<Text>(out var text))
			{
				text.font = asset;
			}
		}
	}
}