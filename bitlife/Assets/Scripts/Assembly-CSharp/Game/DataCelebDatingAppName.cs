using System.Collections.Generic;

namespace Game
{
	public class DataCelebDatingAppName
	{
		private string FirstName__BackingField;

		private string LastName__BackingField;

		private int FirstNameGender__BackingField;

		private string CelebrityCategory__BackingField;

		private int Skin__BackingField;

		private List<string> CountryCodes__BackingField;

		private int CommonnessScore__BackingField;

		public string FirstName
		{
			get
			{
				return FirstName__BackingField;
			}
			set
			{
				FirstName__BackingField = value;
			}
		}

		public string LastName
		{
			get
			{
				return LastName__BackingField;
			}
			set
			{
				LastName__BackingField = value;
			}
		}

		public int FirstNameGender
		{
			get
			{
				return FirstNameGender__BackingField;
			}
			set
			{
				FirstNameGender__BackingField = value;
			}
		}

		public string CelebrityCategory
		{
			get
			{
				return CelebrityCategory__BackingField;
			}
			set
			{
				CelebrityCategory__BackingField = value;
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

		public List<string> CountryCodes
		{
			get
			{
				return CountryCodes__BackingField;
			}
			set
			{
				CountryCodes__BackingField = value;
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
