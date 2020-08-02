using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	using Audio;

	public interface ISound
	{
		ILib.Audio.SoundManagement.BgmController Bgm { get; }
		ILib.Audio.SoundManagement.SeController Se { get; }
	}

	public static class ISoundExtension
	{
		public static ILib.Audio.IPlayingSoundContext PlayHandle(this ISound self, SoundID.Game id)
		{
			if (id == SoundID.Game.None) return null;
			return self.Se.PlayHandle("Game/" + id.ToString());
		}

		public static void Play(this ISound self, SoundID.Game id)
		{
			if (id == SoundID.Game.None) return;
			self.Se.Play("Game/" + id.ToString());
		}

		public static ILib.Audio.IPlayingSoundContext Play(this ISound self, SoundID.Jingle id)
		{
			if (id == SoundID.Jingle.None) return null;
			return self.Se.PlayJingle("Jingle/" + id.ToString());
		}

		public static void Play(this ISound self, SoundID.UI id)
		{
			if (id == SoundID.UI.None) return;
			self.Se.PlaySeFromUI("UI/" + id.ToString());
		}

		public static void Play(this ISound self, SoundID.BGM id, float time = 2f)
		{
			self.Bgm.Change(id.ToString(), time, clearStack: true);
		}

		public static void Push(this ISound self, SoundID.BGM id, float time = 2f)
		{
			self.Bgm.Push(id.ToString(), time);
		}

		public static void Pop(this ISound self, float time)
		{
			self.Bgm.Pop(time);
		}

		public static void Change(this ISound self, SoundID.BGM id, float time)
		{
			self.Bgm.Change(id.ToString(), time);
		}

	}

}