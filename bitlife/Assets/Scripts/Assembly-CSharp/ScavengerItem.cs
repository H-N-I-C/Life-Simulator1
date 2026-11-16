using System;

public sealed class ScavengerItem : IEquatable<string>, IEquatable<ScavengerItem>, IComparable<string>, IComparable<ScavengerItem>
{
	private readonly string ItemName;

	private readonly string InGameHint;

	private readonly string RVGameHint;

	private readonly string AcquisitionMethod;

	private readonly string ScavengerItemEmojiString;

	private readonly HOLIDAY_COLLECTIBLE_TYPE HolidayType;

	private string RedeemId__BackingField;

	public string Name => null;

	public string Hint => null;

	public string RVHint => null;

	public string Requirement => null;

	public string EmojiString => null;

	public HOLIDAY_COLLECTIBLE_TYPE Type => HOLIDAY_COLLECTIBLE_TYPE.NONE;

	public string RedeemId
	{
		get
		{
			return RedeemId__BackingField;
		}
		private set
		{
			RedeemId__BackingField = value;
		}
	}

	public ScavengerItem(ScavengerItemParameters scavengerItemParameters)
	{
	}

	public bool HasCollected()
	{
		return false;
	}

	public Scenario Collect()
	{
		return null;
	}

	public void CollectSetNext(Scenario scenario)
	{
	}

	public Scenario CollectSetNext()
	{
		return null;
	}

	public int CompareTo(string other)
	{
		return 0;
	}

	public bool Equals(string other)
	{
		return false;
	}

	public int CompareTo(ScavengerItem other)
	{
		return 0;
	}

	public bool Equals(ScavengerItem other)
	{
		return false;
	}
}
