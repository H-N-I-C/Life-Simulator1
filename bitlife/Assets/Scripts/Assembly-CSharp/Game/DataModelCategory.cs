namespace Game
{
	public class DataModelCategory
	{
		private string Category__BackingField;

		private MODEL_INDUSTRY Industry__BackingField;

		private MODEL_CAREER_TYPE MinimumModelType__BackingField;

		private int MinTypicalModelAge__BackingField;

		private int MaxTypicalModelAge__BackingField;

		private int CommonnessScore__BackingField;

		public string Category
		{
			get
			{
				return Category__BackingField;
			}
			set
			{
				Category__BackingField = value;
			}
		}

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

		public MODEL_CAREER_TYPE MinimumModelType
		{
			get
			{
				return MinimumModelType__BackingField;
			}
			set
			{
				MinimumModelType__BackingField = value;
			}
		}

		public int MinTypicalModelAge
		{
			get
			{
				return MinTypicalModelAge__BackingField;
			}
			set
			{
				MinTypicalModelAge__BackingField = value;
			}
		}

		public int MaxTypicalModelAge
		{
			get
			{
				return MaxTypicalModelAge__BackingField;
			}
			set
			{
				MaxTypicalModelAge__BackingField = value;
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
