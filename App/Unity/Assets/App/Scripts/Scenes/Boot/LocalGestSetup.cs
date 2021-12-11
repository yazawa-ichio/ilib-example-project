using App.Binding;
using Cysharp.Threading.Tasks;
using ILib.Contents;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
	public class LocalGestSetup : GameScene, IModalContent<InGameParam>
	{
		public async Task<InGameParam> GetModalResult(CancellationToken _)
		{
			var future = new UniTaskCompletionSource<InGameParam>();
			var cancellation = new CancellationTokenSource();
			try
			{
				var vm = new UIGuestSetupVM();
				vm.Message = "参加するルームを選んでください";
				vm.UpdateEnabled = true;
				vm.OnUpdate += () => UpdateRoom(vm, future, cancellation.Token).ConnectUI().Forget();
				vm.OnBack += () =>
				{
					future.TrySetResult(null);
					GameUI.ExecuteBack();
				};
				UpdateRoom(vm, future, cancellation.Token).ConnectUI().Forget();
				var entry = await GameUI.Push("UIGuestSetup", vm);
				return await future.Task;
			}
			finally
			{
				cancellation.Cancel();
			}
		}

		async UniTask UpdateRoom(UIGuestSetupVM vm, UniTaskCompletionSource<InGameParam> future, CancellationToken token)
		{
			if (!vm.UpdateEnabled) return;
			try
			{
				vm.UpdateEnabled = false;
				var rooms = await Realtime.GetRooms(token);
				var list = new List<UIGuestSetupRoomItemVM>(rooms.Length);
				vm.Rooms.Clear();
				foreach (var room in rooms)
				{
					var roomVM = new UIGuestSetupRoomItemVM();
					roomVM.Name = $"{room.No}:{room.Name}";
					roomVM.OnButton += (index) => OnDecision(rooms[index], future, token).Forget();
					vm.Rooms.Add(roomVM);
				}
			}
			finally
			{
				vm.UpdateEnabled = true;
			}
		}

		async UniTask OnDecision(Room room, UniTaskCompletionSource<InGameParam> future, CancellationToken token)
		{
			await Realtime.ConnectHost(room, token);
			future.TrySetResult(new InGameParam
			{
				Mode = InGameMode.Guest
			});
		}

	}
}