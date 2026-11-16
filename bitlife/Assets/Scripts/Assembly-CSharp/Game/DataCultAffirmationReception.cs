using System.Collections.Generic;

namespace Game
{
	public class DataCultAffirmationReception
	{
		private string String__BackingField;

		private List<string> Medium__BackingField;

		private int MinReception__BackingField;

		private int MaxReception__BackingField;

		public string String
		{
			get
			{
				return String__BackingField;
			}
			set
			{
				String__BackingField = value;
			}
		}

		public List<string> Medium
		{
			get
			{
				return Medium__BackingField;
			}
			set
			{
				Medium__BackingField = value;
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
