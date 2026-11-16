using System.Collections.Generic;
using UnityEngine;

public class EmojiCelestialBodyCacheManager
{
	private Dictionary<string, EmojiCelestialBodyCacheItem> _cache;

	private float _div;

	private byte _alphaLimit;

	private byte _full;

	private Vector2 _size;

	private static EmojiCelestialBodyCacheManager SharedInstance;

	public static EmojiCelestialBodyCacheManager SharedEmojiCelestialBodyCacheManager => null;

	public bool ContainsID(string pID)
	{
		return false;
	}

	public string GetEmojiStringForDiscovery(SimDiscovery pDiscovery)
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

	private Sprite CreateCompositeImageForTypePrimaryColorSecondaryColor(string pID)
	{
		return null;
	}

	private Color32[] CreatePlanetComposite(string path, int version, List<Color> colorLayers, bool includeRing)
	{
		return null;
	}

	private Color32[] CreateCometComposite(string path, Color color)
	{
		return null;
	}

	private Color32[] CreateNebulaComposite(string path, Color color)
	{
		return null;
	}

	private Color32[] CreateStarComposite(string path, Color color)
	{
		return null;
	}

	private void CombineTexturesOfSameSize(Color32[] pData1, Color32[] pData2, Color32 pColor, bool pChangeColor)
	{
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Vector2 pData2Offset, Vector2 pData2Size, Color32 pColor, bool pChangeColor, bool imposeAlphaLimits = true)
	{
	}

	private Color32[] RotateTexture(Color32[] pData, float pAngle)
	{
		return null;
	}

	private void FlipTexture(Color32[] pData)
	{
	}

	private void AddTypeForID(string pID)
	{
	}

	private void UpdateWithImageID(Sprite pImage, string pID)
	{
	}

	private void RemoveStaleItems()
	{
	}

	private string GenerateIDForDiscovery(SimDiscovery pDiscovery)
	{
		return null;
	}

	private string GetHexStringFromColor(Color pColor)
	{
		return null;
	}

	private Color32 ConvertColorToColor32(Color pColor)
	{
		return default;
	}

	private Color GetRandomNiceColor(float transparency = 1f)
	{
		return default;
	}

	private string GetSecondaryColor(string pPrimaryColor, bool unrelatedColor = false)
	{
		return null;
	}
}
