using System;
using System.Collections.Generic;

[Serializable]
public class SimBlackMarketVendor
{
	private int Type__BackingField;

	private int NumTimesFought__BackingField;

	private int HeroAgeAtLastFought__BackingField;

	private int NumTimesHaggled__BackingField;

	private int HeroAgeAtLastHaggled__BackingField;

	private int NumTimesThreatenedSomeone__BackingField;

	private int HeroAgeAtLastThreatened__BackingField;

	private int NumBidsWon__BackingField;

	private int NumBidsStarted__BackingField;

	private bool HasRattedOutVendors__BackingField;

	private float Att_attitude__BackingField;

	private int VendorGender__BackingField;

	private int HeroAgeAtLastVisitToVendor__BackingField;

	private bool HeroLastExitedPriorToTakingAction__BackingField;

	private List<SimAuctionItem> _AvailableItems__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int NumTimesFought
	{
		get
		{
			return NumTimesFought__BackingField;
		}
		set
		{
			NumTimesFought__BackingField = value;
		}
	}

	public int HeroAgeAtLastFought
	{
		get
		{
			return HeroAgeAtLastFought__BackingField;
		}
		set
		{
			HeroAgeAtLastFought__BackingField = value;
		}
	}

	public int NumTimesHaggled
	{
		get
		{
			return NumTimesHaggled__BackingField;
		}
		set
		{
			NumTimesHaggled__BackingField = value;
		}
	}

	public int HeroAgeAtLastHaggled
	{
		get
		{
			return HeroAgeAtLastHaggled__BackingField;
		}
		set
		{
			HeroAgeAtLastHaggled__BackingField = value;
		}
	}

	public int NumTimesThreatenedSomeone
	{
		get
		{
			return NumTimesThreatenedSomeone__BackingField;
		}
		set
		{
			NumTimesThreatenedSomeone__BackingField = value;
		}
	}

	public int HeroAgeAtLastThreatened
	{
		get
		{
			return HeroAgeAtLastThreatened__BackingField;
		}
		set
		{
			HeroAgeAtLastThreatened__BackingField = value;
		}
	}

	public int NumBidsWon
	{
		get
		{
			return NumBidsWon__BackingField;
		}
		set
		{
			NumBidsWon__BackingField = value;
		}
	}

	public int NumBidsStarted
	{
		get
		{
			return NumBidsStarted__BackingField;
		}
		set
		{
			NumBidsStarted__BackingField = value;
		}
	}

	public bool HasRattedOutVendors
	{
		get
		{
			return HasRattedOutVendors__BackingField;
		}
		set
		{
			HasRattedOutVendors__BackingField = value;
		}
	}

	public float Att_attitude
	{
		get
		{
			return Att_attitude__BackingField;
		}
		set
		{
			Att_attitude__BackingField = value;
		}
	}

	public int VendorGender
	{
		get
		{
			return VendorGender__BackingField;
		}
		set
		{
			VendorGender__BackingField = value;
		}
	}

	public int HeroAgeAtLastVisitToVendor
	{
		get
		{
			return HeroAgeAtLastVisitToVendor__BackingField;
		}
		set
		{
			HeroAgeAtLastVisitToVendor__BackingField = value;
		}
	}

	public bool HeroLastExitedPriorToTakingAction
	{
		get
		{
			return HeroLastExitedPriorToTakingAction__BackingField;
		}
		set
		{
			HeroLastExitedPriorToTakingAction__BackingField = value;
		}
	}

	public List<SimAuctionItem> _AvailableItems
	{
		get
		{
			return _AvailableItems__BackingField;
		}
		set
		{
			_AvailableItems__BackingField = value;
		}
	}

	public List<SimAuctionItem> AvailableItems
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimBlackMarketVendor(int type, int startingAttitude)
	{
	}

	public string VendorName(bool lowercase = true)
	{
		return null;
	}

	public void ImpactAttitude(int impactScore)
	{
	}

	public void RefreshBlackMarketList()
	{
	}
}
