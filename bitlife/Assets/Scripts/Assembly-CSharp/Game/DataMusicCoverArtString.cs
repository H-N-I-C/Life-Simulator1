using System.Collections.Generic;

namespace Game
{
	public class DataMusicCoverArtString
	{
		private string String__BackingField;

		private List<string> Genre__BackingField;

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
	}
}
