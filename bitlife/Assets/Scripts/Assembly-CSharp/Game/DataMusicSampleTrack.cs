namespace Game
{
	public class DataMusicSampleTrack
	{
		private string Song__BackingField;

		private string Genre__BackingField;

		private int CommonnessScore__BackingField;

		public string Song
		{
			get
			{
				return Song__BackingField;
			}
			set
			{
				Song__BackingField = value;
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
