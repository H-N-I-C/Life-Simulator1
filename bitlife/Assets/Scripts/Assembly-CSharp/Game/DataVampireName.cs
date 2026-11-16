namespace Game
{
	public class DataVampireName
	{
		private PERSON_NAME_TYPE Type__BackingField;

		private string Name__BackingField;

		private int Skin__BackingField;

		public PERSON_NAME_TYPE Type
		{
			get
			{
				return Type__BackingField;
			}
			set
			{
				Type__BackingField = value;
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

		public int Skin
		{
			get
			{
				return Skin__BackingField;
			}
			set
			{
				Skin__BackingField = value;
			}
		}
	}
}
