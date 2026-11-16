using System.Collections.Generic;

namespace Backend.Authentication
{
	public class SeasonsBarMessages
	{
		private List<string> NoSeason__BackingField;

		private List<string> InSeason__BackingField;

		private List<string> NewPlayer__BackingField;

		public List<string> NoSeason
		{
			get
			{
				return NoSeason__BackingField;
			}
			set
			{
				NoSeason__BackingField = value;
			}
		}

		public List<string> InSeason
		{
			get
			{
				return InSeason__BackingField;
			}
			set
			{
				InSeason__BackingField = value;
			}
		}

		public List<string> NewPlayer
		{
			get
			{
				return NewPlayer__BackingField;
			}
			set
			{
				NewPlayer__BackingField = value;
			}
		}
	}
}
