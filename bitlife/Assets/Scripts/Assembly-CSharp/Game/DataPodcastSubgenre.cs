namespace Game
{
	public class DataPodcastSubgenre
	{
		private string Subgenre__BackingField;

		private string Genre__BackingField;

		public string Subgenre
		{
			get
			{
				return Subgenre__BackingField;
			}
			set
			{
				Subgenre__BackingField = value;
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
	}
}
