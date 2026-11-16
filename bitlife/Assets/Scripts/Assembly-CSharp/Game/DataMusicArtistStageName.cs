using System.Collections.Generic;

namespace Game
{
	public class DataMusicArtistStageName
	{
		private string StageName__BackingField;

		private int Gender__BackingField;

		private int Skin__BackingField;

		private List<string> Genre__BackingField;

		private int CommonnessScore__BackingField;

		public string StageName
		{
			get
			{
				return StageName__BackingField;
			}
			set
			{
				StageName__BackingField = value;
			}
		}

		public int Gender
		{
			get
			{
				return Gender__BackingField;
			}
			set
			{
				Gender__BackingField = value;
			}
		}

		public int Skin
		{
			get
			{
				return Skin__BackingField;
			}
			set
			{
				Skin__BackingField = value;
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
