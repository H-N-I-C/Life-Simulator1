using System;
using System.Collections;
using System.Collections.Generic;

namespace CandyBusiness.AlternateIcons
{
	public class AlternateIconController
	{
		[Serializable]
		private class IconConfig
		{
			public List<IconEntry> icons;
		}

		[Serializable]
		private class IconEntry
		{
			public string label;

			public string iconName;
		}

		private const string IconConfigResourcePath = "alternate-icon-names";

		private const string DefaultIconLabel = "Original Icon";

		private static Dictionary<string, string> _iconNames;

		private static Dictionary<string, string> IconNames => null;

		public static void GetAlternateIconList(ArrayList listItems)
		{
		}

		public static void SetIcon(object iconData)
		{
		}

		private static Dictionary<string, string> LoadIconNames()
		{
			return null;
		}

		private static void AddDefaultIcon(Dictionary<string, string> iconNames)
		{
		}
	}
}
