namespace Game
{
	public class MyStuffFunction
	{
		private MyStuffFunctionType Type__BackingField;

		private string StringValue__BackingField;

		private int IntValue__BackingField;

		private string ItemName__BackingField;

		public MyStuffFunctionType Type
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

		public string StringValue
		{
			get
			{
				return StringValue__BackingField;
			}
			set
			{
				StringValue__BackingField = value;
			}
		}

		public int IntValue
		{
			get
			{
				return IntValue__BackingField;
			}
			set
			{
				IntValue__BackingField = value;
			}
		}

		public string ItemName
		{
			get
			{
				return ItemName__BackingField;
			}
			set
			{
				ItemName__BackingField = value;
			}
		}
	}
}
