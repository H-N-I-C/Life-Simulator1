using System.Collections.Generic;

namespace Game
{
	public class DataRacingRaceSponsor
	{
		private string Name__BackingField;

		private bool UseDefiniteArticle__BackingField;

		private List<RACING_TRACK_CATEGORY> SponsorType__BackingField;

		private int MoneyScore__BackingField;

		private int CommonnessScore__BackingField;

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

		public bool UseDefiniteArticle
		{
			get
			{
				return UseDefiniteArticle__BackingField;
			}
			set
			{
				UseDefiniteArticle__BackingField = value;
			}
		}

		public List<RACING_TRACK_CATEGORY> SponsorType
		{
			get
			{
				return SponsorType__BackingField;
			}
			set
			{
				SponsorType__BackingField = value;
			}
		}

		public int MoneyScore
		{
			get
			{
				return MoneyScore__BackingField;
			}
			set
			{
				MoneyScore__BackingField = value;
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
