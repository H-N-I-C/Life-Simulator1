using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorDestination
	{
		private string Location__BackingField;

		private string Country__BackingField;

		private bool UsesDefiniteArticle__BackingField;

		private List<string> Habitat__BackingField;

		private string Activity__BackingField;

		private List<string> Landmarks__BackingField;

		private int CostScore__BackingField;

		private int DangerScore__BackingField;

		private int AppealScore__BackingField;

		private int CommonnessScore__BackingField;

		public string Location
		{
			get
			{
				return Location__BackingField;
			}
			set
			{
				Location__BackingField = value;
			}
		}

		public string Country
		{
			get
			{
				return Country__BackingField;
			}
			set
			{
				Country__BackingField = value;
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

		public List<string> Habitat
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

		public string Activity
		{
			get
			{
				return Activity__BackingField;
			}
			set
			{
				Activity__BackingField = value;
			}
		}

		public List<string> Landmarks
		{
			get
			{
				return Landmarks__BackingField;
			}
			set
			{
				Landmarks__BackingField = value;
			}
		}

		public int CostScore
		{
			get
			{
				return CostScore__BackingField;
			}
			set
			{
				CostScore__BackingField = value;
			}
		}

		public int DangerScore
		{
			get
			{
				return DangerScore__BackingField;
			}
			set
			{
				DangerScore__BackingField = value;
			}
		}

		public int AppealScore
		{
			get
			{
				return AppealScore__BackingField;
			}
			set
			{
				AppealScore__BackingField = value;
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
