using ILib.AssetBundles;
using Cysharp.Threading.Tasks;

namespace App
{
	public interface IResourceLoader
	{
		UniTask Initialize();
		Loading<BundleContainerRef> LoadBundle(string bundleName);
		Loading<T> Load<T>(string path) where T : UnityEngine.Object;
		Loading<T> Load<T>(string bundleName, string assetName) where T : UnityEngine.Object;
		Loading<T> LoadFromId<T>(string id) where T : UnityEngine.Object;
		Loading<bool> LoadScene(string path);
		Loading<bool> LoadScene(string bundleName, string sceneName);
	}
}