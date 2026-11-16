using System.Collections.Generic;

namespace Game
{
	public class DataRacingCupName
	{
		private string Name__BackingField;

		private List<string> RacingCategories__BackingField;

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

		public List<string> RacingCategories
		{
			get
			{
				return RacingCategories__BackingField;
			}
			set
			{
				RacingCategories__BackingField = value;
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
