using Cysharp.Threading.Tasks;
using ILib.Caller;
using ILib.Contents;
using ILib.MVVM;
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
	public class GameManager : Content<GameManagerParam>, IMessengerHook
	{

		public enum Event
		{
			Reboot,
			ReturnTitle,
			WaitInitialize,
		}

		protected override async Task OnBoot()
		{
			Messenger.Default.Hook = this;

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
			Messenger.Default.Hook = null;
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

		void IMessengerHook.OnSend(string name)
		{
			Dispatcher.Broadcast(name);
		}

		void IMessengerHook.OnSend<TEventName>(TEventName name)
		{
			Dispatcher.Broadcast(name);
		}

		void IMessengerHook.OnSend<TMessage>(string name, TMessage args)
		{
			Dispatcher.Broadcast(name, args);
		}

		void IMessengerHook.OnSend<TEventName, UMessage>(TEventName name, UMessage args)
		{
			Dispatcher.Broadcast(name, args);
		}

	}
}