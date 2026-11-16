namespace Genesis.Lists.Models
{
	public class GenesisListBarGraphDefinition
	{
		private string Title__BackingField;

		private string Value__BackingField;

		private string Style__BackingField;

		private int Bubbles__BackingField;

		private bool TwoLine__BackingField;

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
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

		public string Style
		{
			get
			{
				return Style__BackingField;
			}
			set
			{
				Style__BackingField = value;
			}
		}

		public int Bubbles
		{
			get
			{
				return Bubbles__BackingField;
			}
			set
			{
				Bubbles__BackingField = value;
			}
		}

		public bool TwoLine
		{
			get
			{
				return TwoLine__BackingField;
			}
			set
			{
				TwoLine__BackingField = value;
			}
		}
	}
}
