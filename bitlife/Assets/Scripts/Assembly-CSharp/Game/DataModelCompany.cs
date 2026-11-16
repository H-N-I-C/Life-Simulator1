using System.Collections.Generic;

namespace Game
{
	public class DataModelCompany
	{
		private List<string> Format__BackingField;

		private MODEL_INDUSTRY Industry__BackingField;

		private List<string> Prefix__BackingField;

		private List<string> Suffix__BackingField;

		public List<string> Format
		{
			get
			{
				return Format__BackingField;
			}
			set
			{
				Format__BackingField = value;
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

		public List<string> Prefix
		{
			get
			{
				return Prefix__BackingField;
			}
			set
			{
				Prefix__BackingField = value;
			}
		}

		public List<string> Suffix
		{
			get
			{
				return Suffix__BackingField;
			}
			set
			{
				Suffix__BackingField = value;
			}
		}
	}
}
