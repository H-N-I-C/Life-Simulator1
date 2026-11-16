using System.Collections.Generic;

namespace Backend.Authentication
{
	public class AgeRangeSettings
	{
		private bool IsActiveAndroid__BackingField;

		private bool IsActiveiOS__BackingField;

		private int MinAge__BackingField;

		private Dictionary<string, List<string>> AllowedRegions__BackingField;

		public bool IsActiveAndroid
		{
			get
			{
				return IsActiveAndroid__BackingField;
			}
			set
			{
				IsActiveAndroid__BackingField = value;
			}
		}

		public bool IsActiveiOS
		{
			get
			{
				return IsActiveiOS__BackingField;
			}
			set
			{
				IsActiveiOS__BackingField = value;
			}
		}

		public int MinAge
		{
			get
			{
				return MinAge__BackingField;
			}
			set
			{
				MinAge__BackingField = value;
			}
		}

		public Dictionary<string, List<string>> AllowedRegions
		{
			get
			{
				return AllowedRegions__BackingField;
			}
			set
			{
				AllowedRegions__BackingField = value;
			}
		}

		public bool IsActive()
		{
			return false;
		}

		public bool ShouldCheckRegion(string country, string region)
		{
			return false;
		}
	}
}
