namespace Game
{
	public class DataMusicTrackSubject
	{
		private string Subject__BackingField;

		private string Genre__BackingField;

		private int CommonnessScore__BackingField;

		public string Subject
		{
			get
			{
				return Subject__BackingField;
			}
			set
			{
				Subject__BackingField = value;
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
