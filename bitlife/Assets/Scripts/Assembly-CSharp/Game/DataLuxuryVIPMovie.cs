namespace Game
{
	public class DataLuxuryVIPMovie
	{
		private string Title__BackingField;

		private string Genre__BackingField;

		private int CommonnessScore__BackingField;

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

		public string Genre
		{
			get
			{
				return Genre__BackingField;
			}
			set
			{
				Genre__BackingField = value;
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
