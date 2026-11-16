using System.Collections.Generic;

namespace Game
{
	public class DataLuxuryIslandData
	{
		private string IslandType__BackingField;

		private double SmallMinimumCost__BackingField;

		private double SmallMaximumCost__BackingField;

		private double MediumMinimumCost__BackingField;

		private double MediumMaximumCost__BackingField;

		private double LargeMinimumCost__BackingField;

		private double LargeMaximumCost__BackingField;

		private List<string> EligibleCountries__BackingField;

		public string IslandType
		{
			get
			{
				return IslandType__BackingField;
			}
			set
			{
				IslandType__BackingField = value;
			}
		}

		public double SmallMinimumCost
		{
			get
			{
				return SmallMinimumCost__BackingField;
			}
			set
			{
				SmallMinimumCost__BackingField = value;
			}
		}

		public double SmallMaximumCost
		{
			get
			{
				return SmallMaximumCost__BackingField;
			}
			set
			{
				SmallMaximumCost__BackingField = value;
			}
		}

		public double MediumMinimumCost
		{
			get
			{
				return MediumMinimumCost__BackingField;
			}
			set
			{
				MediumMinimumCost__BackingField = value;
			}
		}

		public double MediumMaximumCost
		{
			get
			{
				return MediumMaximumCost__BackingField;
			}
			set
			{
				MediumMaximumCost__BackingField = value;
			}
		}

		public double LargeMinimumCost
		{
			get
			{
				return LargeMinimumCost__BackingField;
			}
			set
			{
				LargeMinimumCost__BackingField = value;
			}
		}

		public double LargeMaximumCost
		{
			get
			{
				return LargeMaximumCost__BackingField;
			}
			set
			{
				LargeMaximumCost__BackingField = value;
			}
		}

		public List<string> EligibleCountries
		{
			get
			{
				return EligibleCountries__BackingField;
			}
			set
			{
				EligibleCountries__BackingField = value;
			}
		}
	}
}
