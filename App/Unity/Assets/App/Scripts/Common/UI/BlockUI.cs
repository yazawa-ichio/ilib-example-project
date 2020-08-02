using Cysharp.Threading.Tasks;
using ILib.ServInject;

namespace App
{
	public readonly struct BlockUIScope : IAsyncScope
	{
		public async UniTask Scope(UniTask task)
		{
			using (ServInjector.Resolve<ISystemUI>().BlockUI())
			{
				await task;
			}
		}

		public async UniTask<T> Scope<T>(UniTask<T> task)
		{
			using (ServInjector.Resolve<ISystemUI>().BlockUI())
			{
				return await task;
			}
		}

	}

	public static class BlockUIExtension
	{
		public static UniTask BlockUI(this UniTask task)
		{
			return default(BlockUIScope).Scope(task);
		}

		public static UniTask<T> BlockUI<T>(this UniTask<T> task)
		{
			return default(BlockUIScope).Scope(task);
		}

	}


}