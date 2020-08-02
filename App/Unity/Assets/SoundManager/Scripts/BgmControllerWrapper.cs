using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ILib.Audio.SoundManagement
{

	public abstract class BgmControllerWrapper<T>
	{
		BgmController Impl => SoundManager.Bgm;

		/// <summary>
		/// Musicのスタック中は音声情報をキャッシュしておくか？
		/// </summary>
		public bool IsCacheInfoInStack { get => Impl.IsCacheInfoInStack; set => Impl.IsCacheInfoInStack = value; }

		/// <summary>
		/// 現在のパラメーター
		/// </summary>
		public string Current => Impl.Current;

		protected abstract string GetParam(T id);

		/// <summary>
		/// 音声を切り替えます。
		/// </summary>
		public void Change(T id, float time = 2, bool clearStack = false)
		{
			Impl.Change(GetParam(id), time, clearStack);
		}

		/// <summary>
		/// 音声を切り替えます。
		/// </summary>
		public void Change(T id, MusicPlayConfig config, bool clearStack = false)
		{
			Impl.Change(GetParam(id), config, clearStack);
		}

		/// <summary>
		/// 現在の音声をスタックに積み
		/// 音声を切り替えます。
		/// </summary>
		public void Push(T id, float time = 2)
		{
			Impl.Push(GetParam(id), time);
		}

		/// <summary>
		/// 現在の音声をスタックに積み
		/// 音声を切り替えます。
		/// </summary>
		public void Push(T id, MusicPlayConfig config)
		{
			Impl.Push(GetParam(id), config);
		}

		/// <summary>
		/// 現在の音声を停止し
		/// スタックの音声に切り替えます。
		/// startLastPositionを有効にするとスタックに積んだ時の再生位置で開始します
		/// </summary>
		public void Pop(float time = 2, bool startLastPosition = false)
		{
			Impl.Pop(time, startLastPosition);
		}

		/// <summary>
		/// 現在の音声を停止し
		/// スタックの音声に切り替えます。
		/// startLastPositionを有効にするとスタックに積んだ時の再生位置で開始します
		/// </summary>
		public void Pop(MusicPlayConfig config, bool startLastPosition = false)
		{
			Impl.Pop(config, startLastPosition);
		}

		/// <summary>
		/// 音声を停止します
		/// </summary>
		public void Stop(float time = 2)
		{
			Impl.Stop(time);
		}

		/// <summary>
		/// 音声を一時停止します。
		/// </summary>
		public void Pause(float time = 0.3F)
		{
			Impl.Pause(time);
		}

		/// <summary>
		/// 音声を一時停止を解除します。
		/// </summary>
		public void Resume(float time = 0.3F)
		{
			Impl.Resume(time);
		}

		/// <summary>
		/// スタックをすべてクリアします
		/// </summary>
		public void ClearStack()
		{
			Impl.ClearStack();
		}

	}

}