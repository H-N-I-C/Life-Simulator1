namespace Game
{
	public class DataPodcastGenre
	{
		private string Genre__BackingField;

		private int PopularityScore__BackingField;

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

		public int PopularityScore
		{
			get
			{
				return PopularityScore__BackingField;
			}
			set
			{
				PopularityScore__BackingField = value;
			}
		}
	}
}
