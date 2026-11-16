namespace Game
{
	public class DataCultFastHungerFood
	{
		private string Food__BackingField;

		private bool Plural__BackingField;

		private bool UsesIndefiniteArticle__BackingField;

		private int CommonnessScore__BackingField;

		public string Food
		{
			get
			{
				return Food__BackingField;
			}
			set
			{
				Food__BackingField = value;
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
