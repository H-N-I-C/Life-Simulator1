using System.Collections.Generic;

namespace Game
{
	public class DataCelebDatingAppAthlete
	{
		private string Sport__BackingField;

		private int Gender__BackingField;

		private float MinimumSalary__BackingField;

		private float MaximumSalary__BackingField;

		private int FameScore__BackingField;

		private int CommonnessScore__BackingField;

		private int RetirementAge__BackingField;

		private List<string> CountryWhitelist__BackingField;

		public string Sport
		{
			get
			{
				return Sport__BackingField;
			}
			set
			{
				Sport__BackingField = value;
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

		public float MinimumSalary
		{
			get
			{
				return MinimumSalary__BackingField;
			}
			set
			{
				MinimumSalary__BackingField = value;
			}
		}

		public float MaximumSalary
		{
			get
			{
				return MaximumSalary__BackingField;
			}
			set
			{
				MaximumSalary__BackingField = value;
			}
		}

		public int FameScore
		{
			get
			{
				return FameScore__BackingField;
			}
			set
			{
				FameScore__BackingField = value;
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

		public int RetirementAge
		{
			get
			{
				return RetirementAge__BackingField;
			}
			set
			{
				RetirementAge__BackingField = value;
			}
		}

		public List<string> CountryWhitelist
		{
			get
			{
				return CountryWhitelist__BackingField;
			}
			set
			{
				CountryWhitelist__BackingField = value;
			}
		}
	}
}
