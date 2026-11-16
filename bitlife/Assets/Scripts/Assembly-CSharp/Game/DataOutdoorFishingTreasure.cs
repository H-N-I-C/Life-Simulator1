using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorFishingTreasure
	{
		private string Name__BackingField;

		private string ListItemName__BackingField;

		private bool UsesDefiniteArticle__BackingField;

		private bool UsesIndefiniteArticle__BackingField;

		private bool Plural__BackingField;

		private int MinValue__BackingField;

		private int MaxValue__BackingField;

		private bool CanBeRefurbished__BackingField;

		private bool Appreciates__BackingField;

		private List<string> Locations__BackingField;

		private int CommonnessScore__BackingField;

		private string EmojiName__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public string ListItemName
		{
			get
			{
				return ListItemName__BackingField;
			}
			set
			{
				ListItemName__BackingField = value;
			}
		}

		public bool UsesDefiniteArticle
		{
			get
			{
				return UsesDefiniteArticle__BackingField;
			}
			set
			{
				UsesDefiniteArticle__BackingField = value;
			}
		}

		public bool UsesIndefiniteArticle
		{
			get
			{
				return UsesIndefiniteArticle__BackingField;
			}
			set
			{
				UsesIndefiniteArticle__BackingField = value;
			}
		}

		public bool Plural
		{
			get
			{
				return Plural__BackingField;
			}
			set
			{
				Plural__BackingField = value;
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

		public bool CanBeRefurbished
		{
			get
			{
				return CanBeRefurbished__BackingField;
			}
			set
			{
				CanBeRefurbished__BackingField = value;
			}
		}

		public bool Appreciates
		{
			get
			{
				return Appreciates__BackingField;
			}
			set
			{
				Appreciates__BackingField = value;
			}
		}

		public List<string> Locations
		{
			get
			{
				return Locations__BackingField;
			}
			set
			{
				Locations__BackingField = value;
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
