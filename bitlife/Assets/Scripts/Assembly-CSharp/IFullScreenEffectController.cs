using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IFullScreenEffectController
{
	IEnumerator StartEffect(RawImage image, Texture2D texture);
}
