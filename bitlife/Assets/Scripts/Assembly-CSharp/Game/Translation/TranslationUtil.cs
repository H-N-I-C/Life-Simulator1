using System.Collections.Generic;

namespace Game.Translation
{
	public class TranslationUtil
	{
		private const string REGEX_JSON_STRING = "\"(?:\\\\.|[^\"\\\\])*\"";

		public static readonly Dictionary<string, string> LanguageToBcp47;

		public static string FixJson(string response)
		{
			return null;
		}

		private static bool TryNormalizeList(string json, out string normalized)
		{
			normalized = null;
			return false;
		}
	}
}
