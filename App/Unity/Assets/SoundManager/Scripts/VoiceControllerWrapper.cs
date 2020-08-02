using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ILib.Audio.SoundManagement
{

	public abstract class VoiceControllerWrapper<T>
	{
		VoiceController Impl => SoundManager.Voice;

		protected abstract string GetParam(T id);

		public virtual IPlayingSoundContext Play(T id)
		{
			return Impl.Play(GetParam(id));
		}

		public virtual IPlayingSoundContext Play(SoundInfo info)
		{
			return Impl.Play(info);
		}

		public virtual void PlayOneShot(T id)
		{
			Impl.PlayOneShot(GetParam(id));
		}

		public virtual void PlayOneShot(SoundInfo info)
		{
			Impl.Play(info);
		}

	}

}