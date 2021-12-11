using App.Binding;
using Cysharp.Threading.Tasks;
using ILib.Caller;
using System;
using System.Threading.Tasks;

namespace App
{
	public class TitleScene : GameScene
	{
		public enum Event
		{
			Start,
			GoToInGame
		}

		public override string GetUnitySceneName()
		{
			return "Title";
		}

		protected override Task OnShutdown()
		{
			return GameUI.UIStack.Pop(GameUI.UIStack.Count);
		}

		protected override void OnCompleteRun()
		{
			GameUI.MainVM = new UITitleVM
			{
				OnStart = Start
			};
		}

		[Handle(Event.Start)]
		void Start()
		{
			GameUI.Push<UISetupVM>("UISetup", (vm) =>
			{
				vm.OnSingle += OnSingle;
				vm.OnLocalGest += OnLocalGest;
				vm.OnLocalHost += OnLocalHost;
			});
		}


		[Handle(Event.GoToInGame)]
		void GoToInGame(InGameParam param)
		{
			Switch<InGameScene>(param);
		}

		void OnSingle()
		{
			GoToInGame(new InGameParam
			{
				Mode = InGameMode.Single,
			});
		}

		async void OnLocalGest()
		{
			try
			{
				var param = await Modal<InGameParam, LocalGestSetup>();
				if (param != null)
				{
					GoToInGame(param);
				}
			}
			catch (Exception ex)
			{
				ThrowException(ex);
			}
		}

		async void OnLocalHost()
		{
			try
			{
				var param = await Modal<InGameParam, LocalHostSetup>();
				if (param != null)
				{
					GoToInGame(param);
				}
			}
			catch (Exception ex)
			{
				ThrowException(ex);
			}
		}

	}

}