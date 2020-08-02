using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ILib.Audio.SoundManagement
{

	public class VoiceController
	{
		ISoundPlayer m_Player;

		public VoiceController(Config config)
		{
			m_Player = config.VoiceProvider.Create(new SoundPlayerConfig { IsCreateIfNotEnough = true });
		}

		public IPlayingSoundContext Play(string prm)
		{
			return m_Player.PlayHandle(prm);
		}

		public IPlayingSoundContext Play(SoundInfo info)
		{
			return m_Player.PlayHandle(info);
		}


		public void PlayOneShot(string prm)
		{
			m_Player.Play(prm);
		}

		public void PlayOneShot(SoundInfo info)
		{
			m_Player.Play(info);
		}

		internal void Release()
		{
			m_Player?.Dispose();
		}

	}

}