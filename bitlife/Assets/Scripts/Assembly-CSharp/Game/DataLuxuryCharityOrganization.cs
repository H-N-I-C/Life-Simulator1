namespace Game
{
	public class DataLuxuryCharityOrganization
	{
		private string Name__BackingField;

		private string Abbreviation__BackingField;

		private bool IsBadCharity__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public string Abbreviation
		{
			get
			{
				return Abbreviation__BackingField;
			}
			set
			{
				Abbreviation__BackingField = value;
			}
		}

		public bool IsBadCharity
		{
			get
			{
				return IsBadCharity__BackingField;
			}
			set
			{
				IsBadCharity__BackingField = value;
			}
		}
	}
}
