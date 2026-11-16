namespace Game
{
	public class DataModelFashionShowVenue
	{
		private string Venue__BackingField;

		private bool UsesIndefiniteArticle__BackingField;

		private int MinProfile__BackingField;

		private int MaxProfile__BackingField;

		public string Venue
		{
			get
			{
				return Venue__BackingField;
			}
			set
			{
				Venue__BackingField = value;
			}
		}

		public bool UsesIndefiniteArticle
		{
			get
			{
				return UsesIndefiniteArticle__BackingField;
			}
			set
			{
				UsesIndefiniteArticle__BackingField = value;
			}
		}

		public int MinProfile
		{
			get
			{
				return MinProfile__BackingField;
			}
			set
			{
				MinProfile__BackingField = value;
			}
		}

		public int MaxProfile
		{
			get
			{
				return MaxProfile__BackingField;
			}
			set
			{
				MaxProfile__BackingField = value;
			}
		}
	}
}
