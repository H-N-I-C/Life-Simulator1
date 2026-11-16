using Genesis.Controllers;

namespace Game
{
	public class GenesisContentMinMax
	{
		private int Min__BackingField;

		private string Value__BackingField;

		private int Max__BackingField;

		public int Min
		{
			get
			{
				return Min__BackingField;
			}
			set
			{
				Min__BackingField = value;
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

		public int Max
		{
			get
			{
				return Max__BackingField;
			}
			set
			{
				Max__BackingField = value;
			}
		}

		public bool IsValid(GenesisMemory memory)
		{
			return false;
		}
	}
}
