using System.Collections.Generic;

namespace Game
{
	public class DataModelAgencyName
	{
		private List<string> Format__BackingField;

		private List<string> Prefix__BackingField;

		private List<string> Suffix__BackingField;

		private int MinAge__BackingField;

		private int MaxAge__BackingField;

		private int TypicalGender__BackingField;

		private int CommonnessScore__BackingField;

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

		public int MinAge
		{
			get
			{
				return MinAge__BackingField;
			}
			set
			{
				MinAge__BackingField = value;
			}
		}

		public int MaxAge
		{
			get
			{
				return MaxAge__BackingField;
			}
			set
			{
				MaxAge__BackingField = value;
			}
		}

		public int TypicalGender
		{
			get
			{
				return TypicalGender__BackingField;
			}
			set
			{
				TypicalGender__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}
	}
}
