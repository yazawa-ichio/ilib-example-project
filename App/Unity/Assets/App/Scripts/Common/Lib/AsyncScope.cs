using Cysharp.Threading.Tasks;

namespace App
{
	public interface IAsyncScope
	{
		UniTask Scope(UniTask task);

		UniTask<T> Scope<T>(UniTask<T> task);
	}

	public static class IAsyncScopeExtension
	{
		public static UniTask Scope<T>(this UniTask task) where T : struct, IAsyncScope
		{
			return default(T).Scope(task);
		}

		public static UniTask<T1> Scope<T1, T2>(this UniTask<T1> task) where T2 : struct, IAsyncScope
		{
			return default(T2).Scope(task);
		}

	}

}