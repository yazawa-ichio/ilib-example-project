using App.Binding;
using Cysharp.Threading.Tasks;
using ILib.Contents;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
	public class LocalHostSetup : GameScene, IModalContent<InGameParam>
	{
		public async Task<InGameParam> GetModalResult(CancellationToken _)
		{
			var cancellation = new CancellationTokenSource();
			var future = new UniTaskCompletionSource<InGameParam>();

			var entry = GameUI.Push<UIHostSetupVM>("UIHostSetup", (vm) =>
			{
				vm.Message = "ルーム名を入力してください";
				vm.RoomName = UnityEngine.Random.Range(0, 99999).ToString().PadLeft(5, '0');
				vm.OnBack += () =>
				{
					cancellation.Cancel();
					future.TrySetResult(null);
					GameUI.ExecuteBack();
				};
				vm.DecisionEnabled = true;
				vm.OnDecision += (x) =>
				{
					vm.DecisionEnabled = false;
					vm.Message = "ユーザー参加を待っています";
					OnDecision(x, future, cancellation.Token).Forget();
				};
			});
			return await future.Task;
		}

		async UniTask OnDecision(string name, UniTaskCompletionSource<InGameParam> future, CancellationToken token)
		{
			await Realtime.StartHost(name, token);
			future.TrySetResult(new InGameParam
			{
				Mode = InGameMode.Host
			});
		}

	}

}