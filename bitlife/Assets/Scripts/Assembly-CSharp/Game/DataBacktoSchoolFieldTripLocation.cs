namespace Game
{
	public class DataBacktoSchoolFieldTripLocation
	{
		private string Location__BackingField;

		private TEACHER_TYPE Theme__BackingField;

		private int HeroMinimumAge__BackingField;

		private int HeroMaximumAge__BackingField;

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

		public TEACHER_TYPE Theme
		{
			get
			{
				return Theme__BackingField;
			}
			set
			{
				Theme__BackingField = value;
			}
		}

		public int HeroMinimumAge
		{
			get
			{
				return HeroMinimumAge__BackingField;
			}
			set
			{
				HeroMinimumAge__BackingField = value;
			}
		}

		public int HeroMaximumAge
		{
			get
			{
				return HeroMaximumAge__BackingField;
			}
			set
			{
				HeroMaximumAge__BackingField = value;
			}
		}
	}
}
