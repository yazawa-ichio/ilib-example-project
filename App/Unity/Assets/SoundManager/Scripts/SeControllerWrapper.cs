using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ILib.Audio.SoundManagement
{

	public abstract class SeControllerWrapper<T>
	{
		SeController Impl => SoundManager.Se;

		//プールを共有する設定なので一つだけの更新でいい
		public int MaxPoolCount
		{
			get => Impl.MaxPoolCount;
			set => Impl.MaxPoolCount = value;
		}

		protected abstract string GetParam(T id);

		/// <summary>
		/// SEを再生します。
		/// </summary>
		public virtual IPlayingSoundContext PlayHandle(T id)
		{
			return Impl.PlayHandle(GetParam(id));
		}

		/// <summary>
		/// SEを再生します。
		/// </summary>
		public virtual void Play(T id)
		{
			Impl.Play(GetParam(id));
		}

		/// <summary>
		/// UI用のSEを再生します。
		/// </summary>
		public virtual void PlaySeFromUI(T id)
		{
			Impl.Play(GetParam(id));
		}

		/// <summary>
		/// UI用のSEを再生します。
		/// </summary>
		public virtual void PlaySeFromUI(T id, out IPlayingSoundContext ctx)
		{
			Impl.PlaySeFromUI(GetParam(id), out ctx);
		}

		/// <summary>
		/// シングルを再生します。
		/// </summary>
		public virtual IPlayingSoundContext PlayJingle(T id)
		{
			return Impl.PlayJingle(GetParam(id));
		}

		/// <summary>
		/// キャッシュにサウンドを追加します。
		/// この関数でロードした場合、参照カウントを持たないためRemoveCache実行時に破棄されます。
		/// CreateCacheScopeで参照カウントがある場合は、参照カウントが0になるまで破棄されません。
		/// Voiceはキャッシュされません。
		/// </summary>
		public virtual void AddCache(T id, System.Action<bool, System.Exception> onLoad)
		{
			Impl.AddCache(GetParam(id), onLoad);
		}

		/// <summary>
		/// キャッシュにサウンドを削除します。
		/// CreateCacheScopeで参照カウントがある場合は、参照カウントが0になるまで破棄されません。
		/// Voiceには影響しません
		/// </summary>
		public virtual void RemoveCache(T id)
		{
			Impl.RemoveCache(GetParam(id));
		}

		/// <summary>
		/// 参照カウント方式でサウンド情報をキャッシュします。
		/// 解放する際は戻り値のDisposeを実行して下さい。
		/// Voiceはキャッシュされません。
		/// </summary>
		public virtual ICacheScope CreateCacheScope(T[] keys)
		{
			return Impl.CreateCacheScope(keys.Select(x => GetParam(x)).ToArray());
		}

		/// <summary>
		/// AddCacheで追加したキャッシュは全て解放します
		/// forceフラグが無効の際は、CreateCacheScopeで参照カウントがある場合は破棄されません。
		/// 有効の際は参照カウントがあっても破棄されます。
		/// </summary>
		public virtual void ClearCache(bool force = false)
		{
			Impl.ClearCache(force);
		}

	}

}