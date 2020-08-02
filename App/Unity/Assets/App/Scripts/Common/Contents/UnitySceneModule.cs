using ILib.Contents;
using ILib.ServInject;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace App
{
	public class UnitySceneModule : Module
	{
		public override ModuleType Type => ModuleType.PreBoot | ModuleType.Shutdown;

		public override async Task OnPreBoot(Content content)
		{
			if (content is GameScene scene)
			{
				string name = scene.GetUnitySceneName();
				if (!string.IsNullOrEmpty(name))
				{
					await ServInjector.Resolve<IResourceLoader>().LoadScene("Scenes/" + name);
					SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
				}
			}
		}

		public override async Task OnShutdown(Content content)
		{
			var scene = content as GameScene;
			if (scene != null)
			{
				string name = scene.GetUnitySceneName();
				if (!string.IsNullOrEmpty(name))
				{
					await SceneManager.UnloadSceneAsync(name);
				}
			}
		}
	}
}