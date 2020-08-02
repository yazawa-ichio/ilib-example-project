using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ILib.MVVM;
using ILib.ServInject;
using ILib.AssetBundles;

namespace App.MVVM
{

	public class LightBundleSpriteBind : LightBind<string, Image>
	{

		IResourceLoader Loader => ServInjector.Resolve<IResourceLoader>();
		Loading<Sprite> m_Loading;

		protected override void UpdateValue(string val)
		{
			m_Loading?.Dispose();
			m_Loading = null;
			m_Target.sprite = null;
			m_Target.enabled = false;
			m_Loading = Loader.Load<Sprite>(val);
			m_Loading.Load(x =>
			{
				m_Target.sprite = x;
				m_Target.enabled = true;
			});
		}
	}

}