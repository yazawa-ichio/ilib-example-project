using Cysharp.Threading.Tasks;
using ILib.Caller;
using ILib.Contents;
using ILib.ServInject;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{

	public class GameManagerParam : IContentParam
	{
		public IContentParam BootScene;

		public Type GetContentType()
		{
			return typeof(GameManager);
		}
	}

	//ゲームのシーンを管理します。
	public class GameManager : Content<GameManagerParam>
	{

		public enum Event
		{
			Reboot,
			ReturnTitle,
			WaitInitialize,
		}

		protected override async Task OnBoot()
		{
			Modules.Add<UnitySceneModule>();
			Modules.Add<ServiceInstallModule>();

			ServInjector.Bind<IDispatcher>(Dispatcher);

			await SceneManager.LoadSceneAsync("Common");

			SceneManager.CreateScene("Empty", new CreateSceneParameters());

			await ServInjector.Resolve<IResourceLoader>().Initialize();

		}

#if DEBUG_BUILD
		protected override async Task OnRun()
		{
			await SceneManager.LoadSceneAsync("Debug", LoadSceneMode.Additive);
		}
#endif

		protected override async Task OnShutdown()
		{
			await SceneManager.UnloadSceneAsync("Common");
			ServInjector.Clear();
		}

		protected override void OnCompleteRun()
		{
			Append(Param.BootScene);
		}

		[Handle(Event.Reboot)]
		void OnReboot()
		{
			Switch<GameManager>(Param);
		}

		protected override bool HandleException(Exception ex)
		{
			Debug.LogException(ex);
			var ui = ILib.ServInject.ServInjector.Resolve<ISystemUI>();
			if (ui == null)
			{
				return base.HandleException(ex);
			}
			else
			{
				ui.Alart(ex.ToString());
				return true;
			}
		}

	}
}