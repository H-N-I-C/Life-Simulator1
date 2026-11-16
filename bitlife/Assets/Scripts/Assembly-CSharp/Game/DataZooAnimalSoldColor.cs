using System.Collections.Generic;

namespace Game
{
	public class DataZooAnimalSoldColor
	{
		private List<string> AnimalType__BackingField;

		private string Snippet__BackingField;

		private int MinHappiness__BackingField;

		private int MaxHappiness__BackingField;

		private int CommonnessScore__BackingField;

		public List<string> AnimalType
		{
			get
			{
				return AnimalType__BackingField;
			}
			set
			{
				AnimalType__BackingField = value;
			}
		}

		public string Snippet
		{
			get
			{
				return Snippet__BackingField;
			}
			set
			{
				Snippet__BackingField = value;
			}
		}

		public int MinHappiness
		{
			get
			{
				return MinHappiness__BackingField;
			}
			set
			{
				MinHappiness__BackingField = value;
			}
		}

		public int MaxHappiness
		{
			get
			{
				return MaxHappiness__BackingField;
			}
			set
			{
				MaxHappiness__BackingField = value;
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
