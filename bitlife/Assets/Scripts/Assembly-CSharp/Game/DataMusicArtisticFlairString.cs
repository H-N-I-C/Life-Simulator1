using System.Collections.Generic;

namespace Game
{
	public class DataMusicArtisticFlairString
	{
		private string String__BackingField;

		private string LogVersion__BackingField;

		private List<string> Genre__BackingField;

		private int ReceptionImpactScore__BackingField;

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

		public List<string> Genre
		{
			get
			{
				return Genre__BackingField;
			}
			set
			{
				Genre__BackingField = value;
			}
		}

		public int ReceptionImpactScore
		{
			get
			{
				return ReceptionImpactScore__BackingField;
			}
			set
			{
				ReceptionImpactScore__BackingField = value;
			}
		}
	}
}
