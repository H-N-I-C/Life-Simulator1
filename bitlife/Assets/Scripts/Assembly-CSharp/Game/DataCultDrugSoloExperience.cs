using System.Collections.Generic;

namespace Game
{
	public class DataCultDrugSoloExperience
	{
		private string StringSnippet__BackingField;

		private string LogVersion__BackingField;

		private List<string> Drug__BackingField;

		private CULT_PHILOSOPHY Philosophy__BackingField;

		private bool PositiveExperience__BackingField;

		private int CommonnessScore__BackingField;

		public string StringSnippet
		{
			get
			{
				return StringSnippet__BackingField;
			}
			set
			{
				StringSnippet__BackingField = value;
			}
		}

		public string LogVersion
		{
			get
			{
				return LogVersion__BackingField;
			}
			set
			{
				LogVersion__BackingField = value;
			}
		}

		public List<string> Drug
		{
			get
			{
				return Drug__BackingField;
			}
			set
			{
				Drug__BackingField = value;
			}
		}

		public CULT_PHILOSOPHY Philosophy
		{
			get
			{
				return Philosophy__BackingField;
			}
			set
			{
				Philosophy__BackingField = value;
			}
		}

		public bool PositiveExperience
		{
			get
			{
				return PositiveExperience__BackingField;
			}
			set
			{
				PositiveExperience__BackingField = value;
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
