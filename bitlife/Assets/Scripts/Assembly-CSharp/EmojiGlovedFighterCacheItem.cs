using System;
using UnityEngine;

public class EmojiGlovedFighterCacheItem
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
		private set
		{
			Image__BackingField = value;
		}
	}

	private string ID
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
		private set
		{
			LastAccessedDate__BackingField = value;
		}
	}

	public EmojiGlovedFighterCacheItem(Sprite image)
	{
	}

	public EmojiGlovedFighterCacheItem(string id)
	{
	}

	public void SetImage(Sprite image)
	{
	}

	public void SetLastAccessedDate(DateTime date)
	{
	}
}
