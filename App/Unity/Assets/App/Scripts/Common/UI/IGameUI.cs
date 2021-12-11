using Cysharp.Threading.Tasks;
using ILib.UI;
using System;
using UVMBinding;

namespace App
{
	public interface IGameUI
	{
		ViewModel MainVM { set; }
		AppUIStack UIStack { get; }
	}

	public static class IGameUIExtension
	{
		public static IStackEntry Push(this IGameUI self, string path, ViewModel vm)
		{
			return self.UIStack.Push(path, vm);
		}

		public static IStackEntry Push<T>(this IGameUI self, string path, Action<T> action) where T : ViewModel, new()
		{
			return self.UIStack.Push(path, action);
		}

		public static IStackEntry Switch(this IGameUI self, string path, ViewModel vm)
		{
			return self.UIStack.Switch(path, vm);
		}

		public static IStackEntry Switch<T>(this IGameUI self, string path, Action<T> action) where T : ViewModel, new()
		{
			return self.UIStack.Switch(path, action);
		}

		public static UniTask Pop(this IGameUI self)
		{
			return self.UIStack.Pop().AsUniTask();
		}

		public static bool Execute<T>(this IGameUI self, Action<T> action)
		{
			return self.UIStack.Execute(action);
		}

		public static bool ExecuteAnyOne<T>(this IGameUI self, Action<T> action)
		{
			return self.UIStack.ExecuteAnyOne(action);
		}

		public static bool ExecuteAnyOne<T>(this IGameUI self, Func<T, bool> action)
		{
			return self.UIStack.ExecuteAnyOne(action);
		}

		public static bool ExecuteBack(this IGameUI self)
		{
			return self.UIStack.ExecuteBack();
		}

	}

}