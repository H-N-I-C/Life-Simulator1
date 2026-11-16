using System.Collections.Generic;
using UnityEngine;

public class EmojiBondCacheManager
{
	private Dictionary<string, EmojiBondCacheItem> mCache;

	private float mDiv;

	private byte mAlphaLimit;

	private byte mFull;

	private Vector2 mSize;

	private static EmojiBondCacheManager SharedInstance;

	public static EmojiBondCacheManager SharedEmojiBondCacheManager => null;

	private Sprite CreateCompositeImageForFlagEmojiString(string flagEmojiString)
	{
		return null;
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Vector2 pData2Offset, Vector2 pData2Size)
	{
	}

	public void UpdateWithImageID(Sprite pImage, string pID)
	{
	}

	public void AddFlagEmojiString(string flagEmojiString)
	{
	}

	public bool ContainsID(string pID)
	{
		return false;
	}

	public bool ContainsFlagEmojiString(string flagEmojiString)
	{
		return false;
	}

	public string GetEmojiStringForFlagEmojiString(string flagEmojiString)
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

	public string GenerateIDForFlagEmojiString(string flagEmojiString)
	{
		return null;
	}
}
