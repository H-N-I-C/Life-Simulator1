using System.Collections.Generic;

namespace Game
{
	public class DataBlackMarketAuctioneerColor
	{
		private string String__BackingField;

		private List<int> AuctionCategory__BackingField;

		private bool Plural__BackingField;

		private int CommonnessScore__BackingField;

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

		public bool Plural
		{
			get
			{
				return Plural__BackingField;
			}
			set
			{
				Plural__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}
	}
}
