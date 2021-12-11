using ILib.AssetBundles;
using ILib.ServInject;
using UnityEngine;
using UnityEngine.UI;
using UVMBinding;

namespace App.Binding
{

	public class LightBundleSpriteBind : Binder<string>
	{

		IResourceLoader Loader => ServInjector.Resolve<IResourceLoader>();
		Loading<Sprite> m_Loading;
		Image m_Target;

		protected override void OnBind()
		{
			TryGetComponent(out m_Target);
		}

		protected override void UpdateValue(string val)
		{
			if (m_Target == null)
			{
				return;
			}
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