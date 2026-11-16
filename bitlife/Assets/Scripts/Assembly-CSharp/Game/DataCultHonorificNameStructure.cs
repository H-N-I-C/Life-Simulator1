namespace Game
{
	public class DataCultHonorificNameStructure
	{
		private string Honorific__BackingField;

		private int Gender__BackingField;

		private int CommonnessScore__BackingField;

		private bool HeroIsLeader__BackingField;

		public string Honorific
		{
			get
			{
				return Honorific__BackingField;
			}
			set
			{
				Honorific__BackingField = value;
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

		public bool HeroIsLeader
		{
			get
			{
				return HeroIsLeader__BackingField;
			}
			set
			{
				HeroIsLeader__BackingField = value;
			}
		}
	}
}
