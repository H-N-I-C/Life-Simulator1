using System.Collections.Generic;

namespace Game
{
	public class DataMusicTrackBeat
	{
		private string Beat__BackingField;

		private List<string> Genre__BackingField;

		private int CommonnessScore__BackingField;

		public string Beat
		{
			get
			{
				return Beat__BackingField;
			}
			set
			{
				Beat__BackingField = value;
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
