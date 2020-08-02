using ILib.AssetBundles;
using ILib.ServInject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Assets
{
	using Cysharp.Threading.Tasks;

	public class ResourceLoader : ServiceMonoBehaviour<IResourceLoader>, IResourceLoader
	{
#if UNITY_EDITOR
		const string ToggleEditorModePath = "Tools/AssetBundle/AssetBundle Load Mode";
		[UnityEditor.MenuItem(ToggleEditorModePath)]
		static void ToggleAssetBundleLoadMode()
		{
			var @checked = UnityEditor.Menu.GetChecked(ToggleEditorModePath);
			UnityEditor.Menu.SetChecked(ToggleEditorModePath, !@checked);
		}
		static bool IsAssetBundleLoadMode()
		{
			return UnityEditor.Menu.GetChecked(ToggleEditorModePath);
		}
#endif

		ISystemUI SystemUI => ServInjector.Resolve<ISystemUI>();

		List<Action<bool>> m_Retry = new List<Action<bool>>();
		LoadingConfig m_Config = new LoadingConfig();

		public UniTask Initialize()
		{
			ServInjector.Inject(this);

			m_Config.ErrorHandle = OnError;
			m_Config.RetryHandle = OnRetry;

			var op = ABLoader.CreateInternalOperator("assets", "manifest", "ExtensionManifestAsset");
#if UNITY_EDITOR
			/*
			var target = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
			var path = "file://" + Application.dataPath.Replace("Assets", "AssetBundles/" + target.ToString());
			var op = ABLoader.CreateNetworkOperator(path, "manifest", System.DateTime.Now.Ticks.ToString(), "ExtensionManifestAsset");
			*/
			ABLoader.UseEditorAsset = !IsAssetBundleLoadMode();
#endif
			var source = new UniTaskCompletionSource();
			ABLoader.Initialize(op, () => source.TrySetResult(), x => source.TrySetException(x));
			return source.Task;
		}

		protected override void OnDestroyEvent()
		{
			ABLoader.Stop();
		}

		string GetBundlePath(string bundleName)
		{
			var name = bundleName.ToLower();
			if (name.IndexOf(".bundle") < 0)
			{
				name += ".bundle";
			}
			return name;
		}

		public Loading<BundleContainerRef> LoadBundle(string bundleName)
		{
			return ABLoader.Load(GetBundlePath(bundleName), m_Config);
		}

		public Loading<T> Load<T>(string path) where T : UnityEngine.Object
		{
			return Load<T>(path, System.IO.Path.GetFileNameWithoutExtension(path));
		}

		public Loading<T> Load<T>(string bundleName, string assetName) where T : UnityEngine.Object
		{
			return ABLoader.LoadAsset<T>(GetBundlePath(bundleName), assetName, m_Config);
		}

		public Loading<T> LoadFromId<T>(string id) where T : UnityEngine.Object
		{
			var (bunde, asset) = ABLoader.GetReference(id);
			return Load<T>(bunde, asset);
		}

		public Loading<bool> LoadScene(string path)
		{
			return LoadScene(path, System.IO.Path.GetFileNameWithoutExtension(path));
		}

		public Loading<bool> LoadScene(string bundleName, string sceneName)
		{
			return ABLoader.LoadScene(GetBundlePath(bundleName), sceneName, m_Config);
		}

		void OnError(Exception ex)
		{
			SystemUI.Alart("リソースのロードに失敗しました。\nタイトルに戻ります。");
		}

		async void OnRetry(ILoading loading, Exception error, Action<bool> retry)
		{
			Debug.LogWarningFormat("Bundle:{0}, Asset:{1}, message:{2}", loading.BundleName, loading.AssetName, error);
			m_Retry.Add(retry);
			if (m_Retry.Count > 1)
			{
				return;
			}
			try
			{
				var ret = await SystemUI.Confirm("リソースのロードに失敗しました。\nリトライしますか？");
				var list = m_Retry.ToArray();
				m_Retry.Clear();
				foreach (var action in list)
				{
					action(ret);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				var list = m_Retry.ToArray();
				m_Retry.Clear();
				foreach (var action in list)
				{
					action(false);
				}
			}
		}


	}
}