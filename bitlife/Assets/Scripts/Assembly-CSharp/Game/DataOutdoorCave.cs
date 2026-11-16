namespace Game
{
	public class DataOutdoorCave
	{
		private string Name__BackingField;

		private string Location__BackingField;

		private int DifficultyScore__BackingField;

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

		public string Location
		{
			get
			{
				return Location__BackingField;
			}
			set
			{
				Location__BackingField = value;
			}
		}

		public int DifficultyScore
		{
			get
			{
				return DifficultyScore__BackingField;
			}
			set
			{
				DifficultyScore__BackingField = value;
			}
		}
	}
}
