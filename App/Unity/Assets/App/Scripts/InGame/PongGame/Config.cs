using ILib.Debugs.AutoRegisters;
using ILib.ServInject;
using UnityEngine.Scripting;

namespace App.InGame
{

	[AutoRegisterTarget(Category = "ゲーム")]
	public class Config
	{
		public static Config I { get; private set; }

		static Config()
		{
			I = new Config();
		}

		private Config() { }

#if DEBUG_BUILD
		[DebugButton("Reset", Path = "コンフィグ"), Preserve]
		void Reset()
		{
			ServInjector.Resolve<App.Debugs.IDebugManager>().Unbind(I);
			I = new Config();
			ServInjector.Resolve<App.Debugs.IDebugManager>().Bind(I);
			ServInjector.Resolve<App.Debugs.IDebugManager>().ReOpen();
		}
#endif

		[DebugSlider("BallSpeed", Path = "コンフィグ", MaxValue = 20, MinValue = 1)]
		public float BallSpeed { get; private set; } = 10;

		[DebugInput("BallSyncInterval", Path = "コンフィグ")]
		public float BallSyncInterval { get; private set; } = 5;

		[DebugSlider("RacketMovePower", Path = "コンフィグ", MaxValue = 1000, MinValue = 100)]
		public float RacketMovePower { get; private set; } = 300;

		[DebugInput("RacketSyncInterval", Path = "コンフィグ")]
		public float RacketSyncInterval { get; private set; } = 5;

	}

}