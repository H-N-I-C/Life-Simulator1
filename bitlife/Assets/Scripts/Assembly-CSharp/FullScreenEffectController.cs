using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class FullScreenEffectController : IFullScreenEffectController
{
	private readonly Func<RawImage, Texture2D, IEnumerator> _EffectUpdate;

	public FullScreenEffectController(Func<RawImage, Texture2D, IEnumerator> effectEnumerator)
	{
	}

	public IEnumerator StartEffect(RawImage image, Texture2D texture)
	{
		return null;
	}
}
