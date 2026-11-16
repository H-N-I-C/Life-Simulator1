using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorCatchableFish
	{
		private string Species__BackingField;

		private string HandbookColorString__BackingField;

		private string Habitat__BackingField;

		private List<string> PreferredBait__BackingField;

		private float MinWeightlbs__BackingField;

		private float MaxWeightlbs__BackingField;

		private int MinValue__BackingField;

		private int MaxValue__BackingField;

		private int DifficultyScore__BackingField;

		private int CommonnessScore__BackingField;

		private string EmojiName__BackingField;

		public string Species
		{
			get
			{
				return Species__BackingField;
			}
			set
			{
				Species__BackingField = value;
			}
		}

		public string HandbookColorString
		{
			get
			{
				return HandbookColorString__BackingField;
			}
			set
			{
				HandbookColorString__BackingField = value;
			}
		}

		public string Habitat
		{
			get
			{
				return Habitat__BackingField;
			}
			set
			{
				Habitat__BackingField = value;
			}
		}

		public List<string> PreferredBait
		{
			get
			{
				return PreferredBait__BackingField;
			}
			set
			{
				PreferredBait__BackingField = value;
			}
		}

		public float MinWeightlbs
		{
			get
			{
				return MinWeightlbs__BackingField;
			}
			set
			{
				MinWeightlbs__BackingField = value;
			}
		}

		public float MaxWeightlbs
		{
			get
			{
				return MaxWeightlbs__BackingField;
			}
			set
			{
				MaxWeightlbs__BackingField = value;
			}
		}

		public int MinValue
		{
			get
			{
				return MinValue__BackingField;
			}
			set
			{
				MinValue__BackingField = value;
			}
		}

		public int MaxValue
		{
			get
			{
				return MaxValue__BackingField;
			}
			set
			{
				MaxValue__BackingField = value;
			}
		}

		public int DifficultyScore
		{
			get
			{
				return DifficultyScore__BackingField;
			}
			set
			{
				DifficultyScore__BackingField = value;
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

		public string EmojiName
		{
			get
			{
				return EmojiName__BackingField;
			}
			set
			{
				EmojiName__BackingField = value;
			}
		}
	}
}
