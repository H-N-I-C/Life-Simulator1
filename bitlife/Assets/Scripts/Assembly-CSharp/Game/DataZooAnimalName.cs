using System.Collections.Generic;

namespace Game
{
	public class DataZooAnimalName
	{
		private string Animal__BackingField;

		private List<string> Breed__BackingField;

		private string Name__BackingField;

		private int Gender__BackingField;

		private int CommonnessScore__BackingField;

		public string Animal
		{
			get
			{
				return Animal__BackingField;
			}
			set
			{
				Animal__BackingField = value;
			}
		}

		public List<string> Breed
		{
			get
			{
				return Breed__BackingField;
			}
			set
			{
				Breed__BackingField = value;
			}
		}

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
