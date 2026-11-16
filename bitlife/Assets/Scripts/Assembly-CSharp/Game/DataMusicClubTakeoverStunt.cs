using System.Collections.Generic;

namespace Game
{
	public class DataMusicClubTakeoverStunt
	{
		private string Scenario__BackingField;

		private string LogVersion__BackingField;

		private List<string> ArtistGenre__BackingField;

		private int PopularityImpact__BackingField;

		private int CommonnessScore__BackingField;

		public string Scenario
		{
			get
			{
				return Scenario__BackingField;
			}
			set
			{
				Scenario__BackingField = value;
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

		public List<string> ArtistGenre
		{
			get
			{
				return ArtistGenre__BackingField;
			}
			set
			{
				ArtistGenre__BackingField = value;
			}
		}

		public int PopularityImpact
		{
			get
			{
				return PopularityImpact__BackingField;
			}
			set
			{
				PopularityImpact__BackingField = value;
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
