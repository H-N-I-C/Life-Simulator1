namespace Game.Seasons.Models
{
	public class SeasonRedeemModel
	{
		private string ItemId__BackingField;

		private int Amount__BackingField;

		private bool IsPremium__BackingField;

		public string ItemId
		{
			get
			{
				return ItemId__BackingField;
			}
			set
			{
				ItemId__BackingField = value;
			}
		}

		public int Amount
		{
			get
			{
				return Amount__BackingField;
			}
			set
			{
				Amount__BackingField = value;
			}
		}

		public bool IsPremium
		{
			get
			{
				return IsPremium__BackingField;
			}
			set
			{
				IsPremium__BackingField = value;
			}
		}
	}
}
