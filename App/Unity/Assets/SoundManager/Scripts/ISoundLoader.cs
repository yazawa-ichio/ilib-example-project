using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ILib.Audio.SoundManagement
{
	public interface ISoundLoader
	{
		bool LoadMusic(string path, Action<MusicInfo, Exception> onLoad);
		bool LoadSound(string path, Action<SoundInfo, Exception> onLoad);
		bool LoadVoice(string path, Action<SoundInfo, Exception> onLoad);
	}
}