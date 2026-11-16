using System.Collections.Generic;

namespace Game
{
	public class DataModelIndustry
	{
		private MODEL_INDUSTRY Industry__BackingField;

		private int CommonnessScore__BackingField;

		private List<string> CountryBlacklist__BackingField;

		public MODEL_INDUSTRY Industry
		{
			get
			{
				return Industry__BackingField;
			}
			set
			{
				Industry__BackingField = value;
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

		public List<string> CountryBlacklist
		{
			get
			{
				return CountryBlacklist__BackingField;
			}
			set
			{
				CountryBlacklist__BackingField = value;
			}
		}
	}
}
