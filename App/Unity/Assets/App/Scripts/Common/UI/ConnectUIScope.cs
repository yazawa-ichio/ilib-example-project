using Cysharp.Threading.Tasks;
using ILib.ServInject;

namespace App
{
	public readonly struct ConnectUIScope : IAsyncScope
	{

		public async UniTask Scope(UniTask task)
		{
			using (ServInjector.Resolve<ISystemUI>().ConnectUI())
			{
				await task;
			}
		}

		public async UniTask<T> Scope<T>(UniTask<T> task)
		{
			using (ServInjector.Resolve<ISystemUI>().ConnectUI())
			{
				return await task;
			}
		}

	}

	public static class ConnectUIExtension
	{
		public static UniTask ConnectUI(this UniTask task)
		{
			return default(ConnectUIScope).Scope(task);
		}

		public static UniTask<T> ConnectUI<T>(this UniTask<T> task)
		{
			return default(ConnectUIScope).Scope(task);
		}

	}

}