using System;
using Cysharp.Threading.Tasks;

namespace App
{
	using UI;

	public interface ISystemUI
	{
		bool IsBlock();
		IDisposable BlockUI();
		void AddBlockCount();
		void RemoveBlockCount();
		IDisposable ConnectUI();
		UniTask Notify(string message);
		UniTask Notify(NotifyParam prm);
		UniTask<bool> Confirm(string message);
		UniTask<bool> Confirm(ConfirmParam prm);
		UniTask Alart();
		UniTask Alart(string message);
		UniTask Alart(AlartParam prm);
	}
}