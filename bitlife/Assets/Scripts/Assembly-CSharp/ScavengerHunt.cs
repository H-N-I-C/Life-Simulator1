using System;
using System.Collections;
using System.Collections.Generic;

public abstract class ScavengerHunt : IReadOnlyCollection<ScavengerItem>, IEnumerable<ScavengerItem>, IEnumerable
{
	private static string OverrideItemId__BackingField;

	private string HuntId__BackingField;

	private string FoundSFXResourceName__BackingField;

	private string CompletedSFXResourceName__BackingField;

	private string Name__BackingField;

	private string AchievementName__BackingField;

	private string EmojiString__BackingField;

	private readonly HOLIDAY_COLLECTIBLE_TYPE ScavengerHuntType__BackingField;

	public static string OverrideItemId
	{
		get
		{
			return OverrideItemId__BackingField;
		}
		set
		{
			OverrideItemId__BackingField = value;
		}
	}

	public string HuntId
	{
		get
		{
			return HuntId__BackingField;
		}
		set
		{
			HuntId__BackingField = value;
		}
	}

	public string FoundSFXResourceName
	{
		get
		{
			return FoundSFXResourceName__BackingField;
		}
		protected set
		{
			FoundSFXResourceName__BackingField = value;
		}
	}

	public string CompletedSFXResourceName
	{
		get
		{
			return CompletedSFXResourceName__BackingField;
		}
		protected set
		{
			CompletedSFXResourceName__BackingField = value;
		}
	}

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		protected set
		{
			Name__BackingField = value;
		}
	}

	public string AchievementName
	{
		get
		{
			return AchievementName__BackingField;
		}
		protected set
		{
			AchievementName__BackingField = value;
		}
	}

	public string EmojiString
	{
		get
		{
			return EmojiString__BackingField;
		}
		protected set
		{
			EmojiString__BackingField = value;
		}
	}

	public HOLIDAY_COLLECTIBLE_TYPE ScavengerHuntType => ScavengerHuntType__BackingField;

	public int Count => 0;

	public ScavengerHunt(HOLIDAY_COLLECTIBLE_TYPE type)
	{
	}

	public bool Contains(string itemName)
	{
		return false;
	}

	public ScavengerItem GetItem(string itemName)
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public virtual DateTime GetStartDate()
	{
		return default;
	}

	public virtual DateTime GetEndDate()
	{
		return default;
	}

	public static string GetId()
	{
		return null;
	}

	public virtual bool IsCurrentlyActive()
	{
		return false;
	}

	public abstract IEnumerator<ScavengerItem> GetEnumerator();

	public abstract DataAccessory GetCompletionAccessory();

	public virtual string GetRedeemCodeByCollectible(HolidayCollectibleMetadata collectible)
	{
		return null;
	}
}
