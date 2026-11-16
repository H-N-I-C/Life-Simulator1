using System;
using UnityEngine;

namespace BitLife.UI.Data
{
	public static class UITheme
	{
		public static string COLOR_FILE_NAME;

		public static string IMAGE_FILE_NAME;

		public static THEME_NAME themeName;

		private static bool useDarkVersion;

		private static DateTime date;

		private static Color colorStep;

		private static ThemeColors_SO colors;

		private static ThemeImages_SO images;

		public static ThemeColors_SO Colors => null;

		public static ThemeImages_SO Images => null;

		static UITheme()
		{
		}

		public static void UpdateThemeName()
		{
		}

		private static void SetThemeName(int themeNameIndex)
		{
		}

		public static Color GetLighterShadeOfColor(Color originalColor)
		{
			return default;
		}

		public static Color ColorForItem(int pItemIndex, int themeOverride = -1)
		{
			return default;
		}

		public static Sprite ImageForItem(int pItemIndex, int themeOverride = -1)
		{
			return null;
		}

		public static Color GetColorFromHexString(string pHexString)
		{
			return default;
		}

		public static string GetHexStringFromColor(Color color)
		{
			return null;
		}
	}
}
