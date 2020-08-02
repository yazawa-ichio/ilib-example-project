
using ILib.Contents;

namespace App
{
	using ILib.UI;
	using System.Threading.Tasks;

	//ゲームに依存しないシステムを管理します
	public class SystemManager : Content
	{

		protected override Task OnBoot()
		{
			ContentsLog.Init();
			UIControlLog.Init();
			ILib.AssetBundles.Logger.Log.Enabled = true;
			ILib.AssetBundles.Logger.Log.Level = ILib.AssetBundles.Logger.Log.LogLevel.All;
			return Util.Successed;
		}


	}

}