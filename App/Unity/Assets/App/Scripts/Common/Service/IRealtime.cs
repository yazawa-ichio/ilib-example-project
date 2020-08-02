using Cysharp.Threading.Tasks;
using SRNet;
using System.Threading;

namespace App
{
	public class Room
	{
		public int No;
		public string Name;
		internal DiscoveryRoom Info;
	}

	public interface IRealtime
	{
		void Send(byte[] obj);
		void Cleanup();
		UniTask StartHost(string name, CancellationToken token);
		UniTask ConnectHost(Room room, CancellationToken token);
		UniTask<Room[]> GetRooms(CancellationToken token);
	}
}