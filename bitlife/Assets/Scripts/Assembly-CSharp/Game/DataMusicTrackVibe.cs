namespace Game
{
	public class DataMusicTrackVibe
	{
		private string Vibe__BackingField;

		private string Genre__BackingField;

		private int CommonnessScore__BackingField;

		public string Vibe
		{
			get
			{
				return Vibe__BackingField;
			}
			set
			{
				Vibe__BackingField = value;
			}
		}

		public string Genre
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
