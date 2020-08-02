using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ILib.Audio.SoundManagement
{

	public class BgmController : IMusicPlayer
	{
		IMusicPlayer m_Player;

		/// <summary>
		/// Musicのスタック中は音声情報をキャッシュしておくか？
		/// </summary>
		public bool IsCacheInfoInStack { get => m_Player.IsCacheInfoInStack; set => m_Player.IsCacheInfoInStack = value; }

		/// <summary>
		/// 現在のパラメーター
		/// </summary>
		public string Current => m_Player.Current;

		int IMusicPlayer<string>.MaxPoolCount { get => m_Player.MaxPoolCount; set => m_Player.MaxPoolCount = value; }

		public BgmController(Config config)
		{
			m_Player = config.BgmProvider.Create();
		}

		void System.IDisposable.Dispose()
		{
			//m_Player?.Dispose();
		}

		internal void Release()
		{
			m_Player?.Dispose();
		}

		/// <summary>
		/// 音声を切り替えます。
		/// </summary>
		public void Change(string prm, float time = 2, bool clearStack = false)
		{
			m_Player.Change(prm, time, clearStack);
		}

		/// <summary>
		/// 音声を切り替えます。
		/// </summary>
		public void Change(string prm, MusicPlayConfig config, bool clearStack = false)
		{
			m_Player.Change(prm, config, clearStack);
		}

		/// <summary>
		/// 現在の音声をスタックに積み
		/// 音声を切り替えます。
		/// </summary>
		public void Push(string prm, float time = 2)
		{
			m_Player.Push(prm, time);
		}

		/// <summary>
		/// 現在の音声をスタックに積み
		/// 音声を切り替えます。
		/// </summary>
		public void Push(string prm, MusicPlayConfig config)
		{
			m_Player.Push(prm, config);
		}

		/// <summary>
		/// 現在の音声を停止し
		/// スタックの音声に切り替えます。
		/// startLastPositionを有効にするとスタックに積んだ時の再生位置で開始します
		/// </summary>
		public void Pop(float time = 2, bool startLastPosition = false)
		{
			m_Player.Pop(time, startLastPosition);
		}

		/// <summary>
		/// 現在の音声を停止し
		/// スタックの音声に切り替えます。
		/// startLastPositionを有効にするとスタックに積んだ時の再生位置で開始します
		/// </summary>
		public void Pop(MusicPlayConfig config, bool startLastPosition = false)
		{
			m_Player.Pop(config, startLastPosition);
		}

		/// <summary>
		/// 音声を停止します
		/// </summary>
		public void Stop(float time = 2)
		{
			m_Player.Stop(time);
		}

		/// <summary>
		/// 音声を一時停止します。
		/// </summary>
		public void Pause(float time = 0.3F)
		{
			m_Player.Pause(time);
		}

		/// <summary>
		/// 音声を一時停止を解除します。
		/// </summary>
		public void Resume(float time = 0.3F)
		{
			m_Player.Resume(time);
		}

		/// <summary>
		/// スタックをすべてクリアします
		/// </summary>
		public void ClearStack()
		{
			m_Player.ClearStack();
		}

	}

}