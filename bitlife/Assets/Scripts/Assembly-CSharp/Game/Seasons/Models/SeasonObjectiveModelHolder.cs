namespace Game.Seasons.Models
{
	public class SeasonObjectiveModelHolder
	{
		private SeasonObjectiveModel _data__BackingField;

		private string Description__BackingField;

		private string Value__BackingField;

		private ObjectiveType ObjectiveType__BackingField;

		private SeasonObjectiveModel _data
		{
			get
			{
				return _data__BackingField;
			}
			set
			{
				_data__BackingField = value;
			}
		}

		public string Description
		{
			get
			{
				return Description__BackingField;
			}
			set
			{
				Description__BackingField = value;
			}
		}

		public string Value
		{
			get
			{
				return Value__BackingField;
			}
			set
			{
				Value__BackingField = value;
			}
		}

		public ObjectiveType ObjectiveType
		{
			get
			{
				return ObjectiveType__BackingField;
			}
			set
			{
				ObjectiveType__BackingField = value;
			}
		}

		public SeasonObjectiveModel GetData()
		{
			return null;
		}
	}
}
