using System;
using System.Collections.Generic;

namespace Game
{
	[Serializable]
	public class SimAuctionHouse
	{
		private string Title__BackingField;

		private int Type__BackingField;

		private int Age__BackingField;

		private string AuctioneerName__BackingField;

		private int AuctioneerGender__BackingField;

		private List<SimAuctionItem> AvailableItems__BackingField;

		private int CrowdSize__BackingField;

		private int Att_Reputation__BackingField;

		private string EmojiString__BackingField;

		private int HeroAgeAtLastAuctionAttend__BackingField;

		[NonSerialized]
		public int BidWarCount;

		[NonSerialized]
		public int BidWarTotal;

		[NonSerialized]
		public SimStreetPerson StorageUnitOwner;

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
			}
		}

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

		public int Age
		{
			get
			{
				return Age__BackingField;
			}
			set
			{
				Age__BackingField = value;
			}
		}

		public string AuctioneerName
		{
			get
			{
				return AuctioneerName__BackingField;
			}
			set
			{
				AuctioneerName__BackingField = value;
			}
		}

		public int AuctioneerGender
		{
			get
			{
				return AuctioneerGender__BackingField;
			}
			set
			{
				AuctioneerGender__BackingField = value;
			}
		}

		public List<SimAuctionItem> AvailableItems
		{
			get
			{
				return AvailableItems__BackingField;
			}
			set
			{
				AvailableItems__BackingField = value;
			}
		}

		public int CrowdSize
		{
			get
			{
				return CrowdSize__BackingField;
			}
			set
			{
				CrowdSize__BackingField = value;
			}
		}

		public int Att_Reputation
		{
			get
			{
				return Att_Reputation__BackingField;
			}
			set
			{
				Att_Reputation__BackingField = value;
			}
		}

		public string EmojiString
		{
			get
			{
				return EmojiString__BackingField;
			}
			set
			{
				EmojiString__BackingField = value;
			}
		}

		public int HeroAgeAtLastAuctionAttend
		{
			get
			{
				return HeroAgeAtLastAuctionAttend__BackingField;
			}
			set
			{
				HeroAgeAtLastAuctionAttend__BackingField = value;
			}
		}

		public SimAuctionHouse(int auctionType, string title)
		{
		}

		public void IncrementAge()
		{
		}

		public void RefreshItemList()
		{
		}

		private void RefreshAvailableItemList()
		{
		}

		public bool CanPlaceBidTimer(int timerLeft)
		{
			return false;
		}

		public bool CanLoopBidWar()
		{
			return false;
		}

		public bool HeroCanWinTheBid(float ratio)
		{
			return false;
		}

		public SimAuctionItem GetItemForAuctionAndRemove()
		{
			return null;
		}
	}
}
