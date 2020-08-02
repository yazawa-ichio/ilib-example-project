
using ILib.Contents;
using ILib.ServInject;

namespace App
{
	public abstract class GameScene : Content
	{
		[Inject]
		public IResourceLoader Loader { get; set; }
		[Inject]
		public ISystemUI SystemUI { get; set; }
		[Inject]
		public IGameUI GameUI { get; set; }
		[Inject]
		public ISound Sound { get; set; }
		[Inject]
		public IRealtime Realtime { get; set; }

		public virtual string GetUnitySceneName() => "";

	}

	public abstract class GameScene<T> : GameScene
	{
		public new T Param => (T)base.Param;
	}

}