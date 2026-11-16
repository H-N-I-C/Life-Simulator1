using System.Collections.Generic;

public class DataBlackMarketPurchaseRequest
{
	private string String__BackingField;

	private int MinFriendliness__BackingField;

	private int MaxFriendliness__BackingField;

	private List<int> AuctionCategory__BackingField;

	private bool ArmsDealerOnly__BackingField;

	private bool WildlifeSmugglerOnly__BackingField;

	public string String
	{
		get
		{
			return String__BackingField;
		}
		set
		{
			String__BackingField = value;
		}
	}

	public int MinFriendliness
	{
		get
		{
			return MinFriendliness__BackingField;
		}
		set
		{
			MinFriendliness__BackingField = value;
		}
	}

	public int MaxFriendliness
	{
		get
		{
			return MaxFriendliness__BackingField;
		}
		set
		{
			MaxFriendliness__BackingField = value;
		}
	}

	public List<int> AuctionCategory
	{
		get
		{
			return AuctionCategory__BackingField;
		}
		set
		{
			AuctionCategory__BackingField = value;
		}
	}

	public bool ArmsDealerOnly
	{
		get
		{
			return ArmsDealerOnly__BackingField;
		}
		set
		{
			ArmsDealerOnly__BackingField = value;
		}
	}

	public bool WildlifeSmugglerOnly
	{
		get
		{
			return WildlifeSmugglerOnly__BackingField;
		}
		set
		{
			WildlifeSmugglerOnly__BackingField = value;
		}
	}
}
