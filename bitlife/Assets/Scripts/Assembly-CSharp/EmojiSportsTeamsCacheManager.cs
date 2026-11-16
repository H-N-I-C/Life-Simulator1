using System.Collections.Generic;
using UnityEngine;

public class EmojiSportsTeamsCacheManager
{
	private Dictionary<string, EmojiSportsTeamsCacheItem> mCache;

	private float mDiv;

	private byte mAlphaLimit;

	private byte mFull;

	private Vector2 mSize;

	private static EmojiSportsTeamsCacheManager SharedInstance;

	public static EmojiSportsTeamsCacheManager SharedEmojiSportsTeamsCacheManager => null;

	private Sprite CreateCompositeImageForTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
	{
		return null;
	}

	public void CombineTextures(Color32[] pData1, Color32[] pData2, Color32 pColor, bool pChangeColor)
	{
	}

	public void UpdateWithImageID(Sprite pImage, string pID)
	{
	}

	public void AddTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
	{
	}

	public bool ContainsID(string pID)
	{
		return false;
	}

	public bool ContainsTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
	{
		return false;
	}

	public string GetEmojiStringForTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
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

	public bool ContainsPrebuiltImageWithTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
	{
		return false;
	}

	public string GenerateIDForTypePrimaryColorSecondaryColor(int pType, Color pPrimaryColor, Color pSecondaryColor)
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

	private string GetHexStringFromColor(Color pColor)
	{
		return null;
	}

	private Color32 ConvertColorToColor32(Color pColor)
	{
		return default;
	}
}
