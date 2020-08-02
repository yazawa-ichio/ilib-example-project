using System.Threading.Tasks;

namespace App
{
	public class BootScene : GameScene
	{
		protected override Task OnRun()
		{
			return Switch<TitleScene>();
		}
	}
}