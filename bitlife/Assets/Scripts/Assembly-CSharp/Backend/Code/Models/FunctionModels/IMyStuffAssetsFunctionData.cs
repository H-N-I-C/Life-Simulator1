namespace Backend.Code.Models.FunctionModels
{
	public abstract class IMyStuffAssetsFunctionData
	{
		private MyStuffFunctionAssetType FunctionType__BackingField;

		private string StringValue1__BackingField;

		private string StringValue2__BackingField;

		private int IntValue1__BackingField;

		private int Model1__BackingField;

		private int Model2__BackingField;

		private int Color__BackingField;

		public MyStuffFunctionAssetType FunctionType
		{
			get
			{
				return FunctionType__BackingField;
			}
			set
			{
				FunctionType__BackingField = value;
			}
		}

		public string StringValue1
		{
			get
			{
				return StringValue1__BackingField;
			}
			set
			{
				StringValue1__BackingField = value;
			}
		}

		public string StringValue2
		{
			get
			{
				return StringValue2__BackingField;
			}
			set
			{
				StringValue2__BackingField = value;
			}
		}

		public int IntValue1
		{
			get
			{
				return IntValue1__BackingField;
			}
			set
			{
				IntValue1__BackingField = value;
			}
		}

		public int Model1
		{
			get
			{
				return Model1__BackingField;
			}
			set
			{
				Model1__BackingField = value;
			}
		}

		public int Model2
		{
			get
			{
				return Model2__BackingField;
			}
			set
			{
				Model2__BackingField = value;
			}
		}

		public int Color
		{
			get
			{
				return Color__BackingField;
			}
			set
			{
				Color__BackingField = value;
			}
		}

		public abstract void Execute();
	}
}
