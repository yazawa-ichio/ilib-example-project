using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace ILib.Audio.SoundManagement
{
	internal class MixerParamTweener
	{
		AudioMixer m_Mixer;
		string m_Param;
		Func<float, float> m_Conv;
		bool m_Removed;
		bool m_Disposed;
		ValueTweener m_Tweener = new ValueTweener(0);
		public string Param => m_Param;

		public MixerParamTweener(AudioMixer mixer, string param, float start, Func<float, float> conv)
		{
			m_Mixer = mixer;
			m_Param = param;
			m_Conv = conv;
			m_Tweener = new ValueTweener(start);
		}

		public void Start(float target, float time = 0.2f)
		{
			m_Tweener.Start(target, time);
		}

		public void Dispose()
		{
			if (m_Disposed) return;
			m_Disposed = true;
		}

		public bool Update()
		{
			if (m_Tweener.IsRunning)
			{
				var ret = m_Tweener.Get(Time.unscaledDeltaTime);
				m_Mixer.SetFloat(m_Param, m_Conv(ret));
				return true;
			}
			return false;
		}
	}
}