namespace Game
{
	public class DataCultDrumCircleArtist
	{
		private string ArtistName__BackingField;

		private int EngagementScore__BackingField;

		private CULT_PHILOSOPHY PhilosophyEngagementBonus__BackingField;

		public string ArtistName
		{
			get
			{
				return ArtistName__BackingField;
			}
			set
			{
				ArtistName__BackingField = value;
			}
		}

		public int EngagementScore
		{
			get
			{
				return EngagementScore__BackingField;
			}
			set
			{
				EngagementScore__BackingField = value;
			}
		}

		public CULT_PHILOSOPHY PhilosophyEngagementBonus
		{
			get
			{
				return PhilosophyEngagementBonus__BackingField;
			}
			set
			{
				PhilosophyEngagementBonus__BackingField = value;
			}
		}
	}
}
