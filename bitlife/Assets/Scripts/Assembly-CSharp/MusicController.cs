using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public float targetVolume;

		public MusicController _003C_003E4__this;

		public Action onComplete;

		internal float _003CFadeSoundLevel_003Eb__0()
		{
			return 0f;
		}

		internal void _003CFadeSoundLevel_003Eb__1(float x)
		{
		}

		internal void _003CFadeSoundLevel_003Eb__2()
		{
		}

		internal void _003CFadeSoundLevel_003Eb__3()
		{
		}
	}

	private AudioSource _audioSource;

	private float _maximumSoundLevel;

	private TweenerCore<float, float, FloatOptions> _tween;

	private string _lastCachedResourceName;

	private void Init(AudioSource source)
	{
	}

	public void LoadResourceMusic(string resourceName)
	{
	}

	private void OnDestroy()
	{
	}

	public void SetMusicLevel(float level)
	{
	}

	public void StartMusic()
	{
	}

	public void StopMusic()
	{
	}

	public static MusicController CreateInstance(string gameObjectName)
	{
		return null;
	}

	private void FadeSoundLevel(float level, Action onComplete = null)
	{
	}

	private void _003CStopMusic_003Eb__9_0()
	{
	}
}
