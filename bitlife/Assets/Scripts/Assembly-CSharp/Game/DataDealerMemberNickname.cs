namespace Game
{
	public class DataDealerMemberNickname
	{
		private string Nickname__BackingField;

		private int Gender__BackingField;

		private bool KingpinOnly__BackingField;

		private int CommonnessScore__BackingField;

		public string Nickname
		{
			get
			{
				return Nickname__BackingField;
			}
			set
			{
				Nickname__BackingField = value;
			}
		}

		public int Gender
		{
			get
			{
				return Gender__BackingField;
			}
			set
			{
				Gender__BackingField = value;
			}
		}

		public bool KingpinOnly
		{
			get
			{
				return KingpinOnly__BackingField;
			}
			set
			{
				KingpinOnly__BackingField = value;
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
