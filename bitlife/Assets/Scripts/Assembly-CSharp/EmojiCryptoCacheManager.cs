using System.Collections.Generic;
using UnityEngine;

public class EmojiCryptoCacheManager
{
	private Dictionary<string, EmojiCryptoCacheItem> mCache;

	private float mDiv;

	private byte mAlphaLimit;

	private byte mFull;

	private Vector2 mSize;

	private static EmojiCryptoCacheManager SharedInstance;

	public static EmojiCryptoCacheManager SharedEmojiCryptoCacheManager => null;

	private Sprite CreateCompositeImageForTextString(string textString)
	{
		return null;
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Vector2 pData2Offset, Vector2 pData2Size)
	{
	}

	public void UpdateWithImageID(Sprite pImage, string pID)
	{
	}

	public void AddTextString(string textString)
	{
	}

	public bool ContainsID(string pID)
	{
		return false;
	}

	public bool ContainsTextString(string textString)
	{
		return false;
	}

	public string GetEmojiStringForTextString(string textString)
	{
		return null;
	}

	public Sprite GetImageWithID(string pID)
	{
		return null;
	}

	public bool ContainsPrebuiltImageWithID(string pID)
	{
		return false;
	}

	public bool ContainsIDWithUnbuiltImage(string id)
	{
		return false;
	}

	public string GenerateIDForTextString(string textString)
	{
		return null;
	}
}
