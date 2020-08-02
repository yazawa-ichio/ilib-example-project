using App.MVVM;
using Cysharp.Threading.Tasks;
using ILib.Caller;
using ILib.ServInject;
using UnityEngine;

namespace App.UI
{


	public class SystemUI : ISystemUI
	{
		SystemUIQueue m_Queue;
		GameObject m_InputBlock;
		RefCountToggle m_BlockToggle;
		GameObject m_Connect;
		RefCountToggle m_ConnectToggle;

		public SystemUI(SystemUIQueue queue, GameObject inputBlock, GameObject connect)
		{
			m_Queue = queue;
			m_InputBlock = inputBlock;
			m_Connect = connect;
			m_InputBlock.SetActive(false);
			m_Connect.SetActive(false);

			m_BlockToggle = new RefCountToggle();
			m_BlockToggle.OnChange += (x) => { if (m_InputBlock != null) m_InputBlock.SetActive(x); };
			m_ConnectToggle = new RefCountToggle();
			m_ConnectToggle.OnChange += (x) => { if (m_Connect != null) m_Connect.SetActive(x); };
		}

		public bool IsBlock() => m_InputBlock.activeSelf;

		public System.IDisposable BlockUI() => m_BlockToggle.CrateRefHandle();

		public System.IDisposable ConnectUI() => m_ConnectToggle.CrateRefHandle();

		public void AddBlockCount() => m_BlockToggle.AddRef();

		public void RemoveBlockCount() => m_BlockToggle.RemoveRef();

		public bool HasProcess() => m_Queue.HasProcess;

		public bool ExecuteBack() => m_Queue.ExecuteBack();

		public UniTask Alart()
		{
			return Alart("エラーが発生しました。\nタイトルに戻ります");
		}

		public UniTask Alart(string message)
		{
			return Alart(new AlartParam { Message = message });
		}

		public async UniTask Alart(AlartParam prm)
		{
			try
			{
				var vm = new UIDialogVM();
				vm.ActiveConfirm = false;
				vm.ActiveNofify = true;
				vm.TitleText = prm.Title;
				vm.MessageText = prm.Message;
				var entry = m_Queue.Enqueue("UIDialog", vm);
				entry.IsWaitCloseCompleted = prm.Reboot;
				vm.OnButtonOk += async () =>
				{
					if (prm.Reboot)
					{
						await entry.Close();
						ServInjector.Resolve<IDispatcher>().Message(GameManager.Event.Reboot);
					}
					else
					{
						await entry.Close();
					}
				};
				await entry;
			}
			catch (System.Exception ex)
			{
				Debug.LogWarning(ex);
				throw;
			}
		}

		public UniTask Notify(string message)
		{
			return Notify(new NotifyParam { Message = message });
		}

		public async UniTask Notify(NotifyParam prm)
		{
			var vm = new UIDialogVM();
			vm.ActiveConfirm = false;
			vm.ActiveNofify = true;
			vm.TitleText = prm.Title;
			vm.MessageText = prm.Message;
			var entry = m_Queue.Enqueue("UIDialog", vm);
			vm.OnButtonOk += () =>
			{
				entry.Close();
			};
			if (prm.UseMaskBack)
			{
				vm.OnButtonMask += () =>
				{
					entry.Close();
				};
			}
			await entry;
		}


		public UniTask<bool> Confirm(string message)
		{
			return Confirm(new ConfirmParam { Message = message });
		}

		public async UniTask<bool> Confirm(ConfirmParam prm)
		{
			var vm = new UIDialogVM();
			vm.ActiveConfirm = true;
			vm.ActiveNofify = false;
			vm.TitleText = prm.Title;
			vm.MessageText = prm.Message;
			var entry = m_Queue.Enqueue("UIDialog", vm);
			var ret = false;
			vm.OnButtonYes += () =>
			{
				ret = true;
				entry.Close();
			};
			vm.OnButonNo += () =>
			{
				ret = false;
				entry.Close();
			};
			await entry;
			return ret;
		}
	}

}