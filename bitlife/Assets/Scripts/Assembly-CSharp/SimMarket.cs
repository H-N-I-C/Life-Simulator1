using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimMarket
{
	private sealed class _003C_003Ec__DisplayClass25_0
	{
		public SimAuctionHouse auctionHouse;

		internal bool _003CCanParticipateHouseAuction_003Eb__0(SimAuctionHouse t)
		{
			return false;
		}
	}

	private SimBlackMarket BlackMarket__BackingField;

	private SimMuseum Museum__BackingField;

	private int NumberWonHouseAuctions__BackingField;

	private List<SimAuctionHouse> _AvailableAuctionHouses__BackingField;

	private int HeroAgeAtLastRefresh__BackingField;

	public SimBlackMarket BlackMarket
	{
		get
		{
			return BlackMarket__BackingField;
		}
		set
		{
			BlackMarket__BackingField = value;
		}
	}

	public SimMuseum Museum
	{
		get
		{
			return Museum__BackingField;
		}
		set
		{
			Museum__BackingField = value;
		}
	}

	public int NumberWonHouseAuctions
	{
		get
		{
			return NumberWonHouseAuctions__BackingField;
		}
		set
		{
			NumberWonHouseAuctions__BackingField = value;
		}
	}

	private List<SimAuctionHouse> _AvailableAuctionHouses
	{
		get
		{
			return _AvailableAuctionHouses__BackingField;
		}
		set
		{
			_AvailableAuctionHouses__BackingField = value;
		}
	}

	public List<SimAuctionHouse> AvailableAuctionHouses
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int HeroAgeAtLastRefresh
	{
		get
		{
			return HeroAgeAtLastRefresh__BackingField;
		}
		set
		{
			HeroAgeAtLastRefresh__BackingField = value;
		}
	}

	public void RefreshMarket()
	{
	}

	public void IncrementAge()
	{
	}

	public bool CanParticipateHouseAuction(SimAuctionHouse auctionHouse)
	{
		return false;
	}

	public bool CanParticipateWithRewardedAd()
	{
		return false;
	}

	public bool CanParticipateHouseAuction(int auctionHouse)
	{
		return false;
	}
}
