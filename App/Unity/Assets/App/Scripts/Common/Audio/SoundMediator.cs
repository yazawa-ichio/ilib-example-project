using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILib.Audio.SoundManagement;
using ILib.Audio;
using ILib.ServInject;
using ILib;
using System;

namespace App.Audio
{
	using Audio;
	using UnityEngine.Audio;

	public class SoundMediator : ServiceMonoBehaviour<ISound>, ISoundLoader, ISound
	{
		public BgmController Bgm => ILib.Audio.SoundManager.Bgm;

		public SeController Se => ILib.Audio.SoundManager.Se;

		IResourceLoader Loader => ServInjector.Resolve<IResourceLoader>();

		public bool LoadMusic(string path, Action<MusicInfo, Exception> onLoad)
		{
			var loading = Loader.Load<MusicData>("Audio/Music/" + path);
			loading.Load(x =>
			{
				onLoad(x.CreateMusic(), null);
			});
			return true;
		}

		public bool LoadSound(string path, Action<SoundInfo, Exception> onLoad)
		{
			var loading = Loader.Load<SoundData>("Audio/SE/" + path);
			loading.Load(x =>
			{
				onLoad(x.CreateInfo(), null);
			});
			return true;
		}

		public bool LoadVoice(string path, Action<SoundInfo, Exception> onLoad)
		{
			return false;
		}
	}

}