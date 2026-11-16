using System;
using UnityEngine;

public class EmojiCelestialBodyCacheItem
{
	private Sprite Image__BackingField;

	private string ID__BackingField;

	private DateTime LastAccessedDate__BackingField;

	public Sprite Image
	{
		get
		{
			return Image__BackingField;
		}
		set
		{
			Image__BackingField = value;
		}
	}

	public string ID
	{
		get
		{
			return ID__BackingField;
		}
		set
		{
			ID__BackingField = value;
		}
	}

	public DateTime LastAccessedDate
	{
		get
		{
			return LastAccessedDate__BackingField;
		}
		set
		{
			LastAccessedDate__BackingField = value;
		}
	}

	public EmojiCelestialBodyCacheItem(Sprite pImage)
	{
	}

	public EmojiCelestialBodyCacheItem(string id)
	{
	}

	public void SetImage(Sprite pImage)
	{
	}

	public void SetLastAccessedDate(DateTime pDate)
	{
	}
}
