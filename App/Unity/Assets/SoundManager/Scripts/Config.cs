using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ILib.Audio.SoundManagement
{

	public class Config
	{
		public int InitPoolCount = 32;

		public AudioMixer Mixer;

		public IMusicProvider BgmProvider;

		public ISoundProvider<string> GameSeProvider;

		public ISoundProvider<string> UISEProvider;

		public ISoundProvider<string> JingleProvider;

		public ISoundProvider<string> VoiceProvider;

	}

}