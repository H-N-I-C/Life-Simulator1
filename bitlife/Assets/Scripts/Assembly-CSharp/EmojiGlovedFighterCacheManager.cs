using System.Collections.Generic;
using UnityEngine;

public class EmojiGlovedFighterCacheManager
{
	private readonly Dictionary<string, EmojiGlovedFighterCacheItem> _cache;

	private const float Div = 1f / 255f;

	private const byte AlphaLimit = 63;

	private const byte Full = 255;

	private Vector2 _size;

	private static EmojiGlovedFighterCacheManager _sharedInstance;

	public static EmojiGlovedFighterCacheManager SharedEmojiGlovedFighterCacheManager => null;

	public bool ContainsID(string id)
	{
		return false;
	}

	public string GetEmojiStringForFighter(SimFightClubFighter fighter)
	{
		return null;
	}

	public Sprite GetImageWithID(string id)
	{
		return null;
	}

	public bool ContainsPrebuiltImageWithID(string id)
	{
		return false;
	}

	public bool ContainsIDWithUnbuiltImage(string id)
	{
		return false;
	}

	private Sprite CreateCompositeImage(string id)
	{
		return null;
	}

	private Color32[] CreateCompositeFaceAndGloves(string id)
	{
		return null;
	}

	private void CombineTextures(Color32[] data1, Color32[] data2, Vector2 data2Offset, Vector2 data2Size)
	{
	}

	private void AddTypeForID(string id)
	{
	}

	private void UpdateWithImageID(Sprite image, string id)
	{
	}

	private void RemoveStaleItems()
	{
	}

	private static string GenerateIDForFighter(SimFightClubFighter fighter)
	{
		return null;
	}

	private static string GetIDPrefix()
	{
		return null;
	}

	private static string GetColorForColorID(string id)
	{
		return null;
	}
}
