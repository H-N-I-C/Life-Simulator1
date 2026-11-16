using System.Collections.Generic;

namespace Game
{
	public class DataZooFundraiserColor
	{
		private string Type__BackingField;

		private List<string> AnimalType__BackingField;

		private string Snippet__BackingField;

		private int MinReception__BackingField;

		private int MaxReception__BackingField;

		public string Type
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

		public List<string> AnimalType
		{
			get
			{
				return AnimalType__BackingField;
			}
			set
			{
				AnimalType__BackingField = value;
			}
		}

		public string Snippet
		{
			get
			{
				return Snippet__BackingField;
			}
			set
			{
				Snippet__BackingField = value;
			}
		}

		public int MinReception
		{
			get
			{
				return MinReception__BackingField;
			}
			set
			{
				MinReception__BackingField = value;
			}
		}

		public int MaxReception
		{
			get
			{
				return MaxReception__BackingField;
			}
			set
			{
				MaxReception__BackingField = value;
			}
		}
	}
}
