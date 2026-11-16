using System.Collections.Generic;

namespace Game
{
	public class DataMusicSoundCloudTrackInstrument
	{
		private string Instrument__BackingField;

		private List<string> Genre__BackingField;

		private List<string> ProjectTones__BackingField;

		private int CommonnessScore__BackingField;

		public string Instrument
		{
			get
			{
				return Instrument__BackingField;
			}
			set
			{
				Instrument__BackingField = value;
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

		public List<string> ProjectTones
		{
			get
			{
				return ProjectTones__BackingField;
			}
			set
			{
				ProjectTones__BackingField = value;
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
