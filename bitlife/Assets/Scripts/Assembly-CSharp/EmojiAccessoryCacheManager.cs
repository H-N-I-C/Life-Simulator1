using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiAccessoryCacheManager
{
	private Dictionary<string, EmojiAccessoryCacheItem> mCache;

	private float mDiv;

	private byte mAlphaLimit;

	private byte mFull;

	private Vector2 mSize;

	private ArrayList mZooAccessoryHeadwearIndexes;

	private static EmojiAccessoryCacheManager SharedInstance;

	public static EmojiAccessoryCacheManager SharedEmojiAccessoryCacheManager => null;

	private Sprite CreateCompositeImageForTypeIndex(int pType, int pIndex)
	{
		return null;
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Vector2 pData2Offset, Vector2 pData2Size)
	{
	}

	public void UpdateWithImageID(Sprite pImage, string pID)
	{
	}

	public void AddTypeIndex(int pType, int pIndex)
	{
	}

	public bool ContainsID(string pID)
	{
		return false;
	}

	public bool ContainsTypeIndex(int pType, int pIndex)
	{
		return false;
	}

	public string GetEmojiStringForTypeIndex(int pType, int pIndex)
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

	public bool ContainsPrebuiltImageWithTypeIndex(int pType, int pIndex)
	{
		return false;
	}

	public string GenerateIDForTypeIndex(int pType, int pIndex)
	{
		return null;
	}

	public int GetTypeFromID(string pID)
	{
		return 0;
	}

	public int GetIndexFromID(string pID)
	{
		return 0;
	}
}
