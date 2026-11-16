using System.Collections.Generic;

namespace Game.Seasons.Models
{
	public class SeasonRedeemResponseAttributes
	{
		private int? PlayerCurrency__BackingField;

		private List<SeasonRedeemModel> Redeemed__BackingField;

		public int? PlayerCurrency
		{
			get
			{
				return PlayerCurrency__BackingField;
			}
			set
			{
				PlayerCurrency__BackingField = value;
			}
		}

		public List<SeasonRedeemModel> Redeemed
		{
			get
			{
				return Redeemed__BackingField;
			}
			set
			{
				Redeemed__BackingField = value;
			}
		}
	}
}
