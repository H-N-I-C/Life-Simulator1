using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class FullScreenEffect : IFullScreenEffectController
{
	public static readonly IFullScreenEffectController Wobble;

	public abstract IEnumerator StartEffect(RawImage image, Texture2D texture);
}
