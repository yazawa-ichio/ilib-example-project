using System.Collections;
using System.Threading.Tasks;
using ILib.Contents;
using ILib.ServInject;

namespace App
{
	public class ServiceInstallModule : Module
	{
		public override ModuleType Type => ModuleType.PreBoot;

		public override Task OnPreBoot(Content content)
		{
			ServInjector.Inject(content);
			return Util.Successed;
		}
	}
}