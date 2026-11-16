using System.Collections.Generic;

namespace Game
{
	public class DataMusicSoundCloudComment
	{
		private string String__BackingField;

		private string LogVersion__BackingField;

		private List<string> TrackGenre__BackingField;

		private int MinPopularity__BackingField;

		private int MaxPopularity__BackingField;

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

		public List<string> TrackGenre
		{
			get
			{
				return TrackGenre__BackingField;
			}
			set
			{
				TrackGenre__BackingField = value;
			}
		}

		public int MinPopularity
		{
			get
			{
				return MinPopularity__BackingField;
			}
			set
			{
				MinPopularity__BackingField = value;
			}
		}

		public int MaxPopularity
		{
			get
			{
				return MaxPopularity__BackingField;
			}
			set
			{
				MaxPopularity__BackingField = value;
			}
		}
	}
}
