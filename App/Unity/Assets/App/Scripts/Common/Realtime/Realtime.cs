using Cysharp.Threading.Tasks;
using ILib.Caller;
using ILib.ServInject;
using SRConnection;
using SRConnection.Unity;
using System;
using System.Linq;
using System.Threading;

namespace App.Net
{
	public enum RealtimeEvent
	{
		Receive,
		Disconnect,
	}

	public class Realtime : ServiceMonoBehaviour<IRealtime>, IRealtime
	{
		Peer m_Peer;
		P2PClient m_Client;

		public void Send(byte[] data)
		{
			m_Peer?.Send(data);
		}

		public void Cleanup()
		{
			var client = m_Client;
			m_Client = null;
			m_Peer = null;
			if (client != null)
			{
				client.OnDisconnect -= OnDisconnect;
				client.OnRemovePeer -= OnRemovePeer;
				Destroy(client);
			}
		}

		P2PClient CreateClient()
		{
			var client = gameObject.AddComponent<P2PClient>();
			client.OnDisconnect += OnDisconnect;
			client.OnRemovePeer += OnRemovePeer;
			client.OnMessage += OnMessage;
			client.OnAddPeer += (peer) =>
			{
				if (m_Peer == null)
				{
					m_Peer = peer;
				}
				else
				{
					peer.Disconnect();
				}
			};
			return m_Client = client;
		}

		public UniTask StartHost(string name, CancellationToken token)
		{
			var future = new UniTaskCompletionSource();
			var client = CreateClient();
			client.StartHost(name);
			Action<Peer> action = null;
			action = (_) =>
			{
				client.OnAddPeer -= action;
				future.TrySetResult();
			};
			client.OnAddPeer += action;
			token.Register(() =>
			{
				if (future.Task.Status != UniTaskStatus.Succeeded)
				{
					future.TrySetCanceled(token);
					Cleanup();
				}
			});
			return future.Task;
		}


		public async UniTask ConnectHost(Room room, CancellationToken token)
		{
			Cleanup();
			try
			{
				var client = CreateClient();
				await client.ConnectHost(room.Info, token);
			}
			catch
			{
				Cleanup();
			}
		}

		public async UniTask<Room[]> GetRooms(CancellationToken token)
		{
			try
			{
				var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(2));
				token.Register(() => cancellation.Cancel());
				var rooms = await DiscoveryUtil.GetRooms(cancellation.Token);
				return rooms.Select((x, y) => new Room
				{
					No = y,
					Name = x.Name,
					Info = x,
				}).ToArray();
			}
			catch
			{
				return System.Array.Empty<Room>();
			}
		}

		void OnMessage(Peer peer, byte[] data)
		{
			ServInjector.Resolve<IDispatcher>().Broadcast(RealtimeEvent.Receive, data);
		}

		void OnRemovePeer(Peer peer)
		{
			if (m_Peer == peer)
			{
				m_Client.Disconnect();
			}
		}

		void OnDisconnect()
		{
			if (m_Client != null)
			{
				ServInjector.Resolve<IDispatcher>().Broadcast(RealtimeEvent.Disconnect);
			}
			Cleanup();
		}

	}
}