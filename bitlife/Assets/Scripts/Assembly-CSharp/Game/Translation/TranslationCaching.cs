using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

namespace Game.Translation
{
	public class TranslationCaching
	{
		private static Dictionary<ulong, string> cache;

		private static List<string> englishMiss;

		private static HashSet<string> firstNameLookup;

		private static HashSet<string> lastNameLookup;

		private static bool nameLookupLoaded;

		private static bool isLoaded;

		private static TranslationDomains _domains;

		public static UnityEvent<string, string> OnCacheHit;

		public static UnityEvent<string> OnCacheMiss;

		public static int CacheSize => 0;

		public static string GetTranslation(string text)
		{
			return null;
		}

		private static bool TryGetTaggedTextTranslation(string text, out string translated)
		{
			translated = null;
			return false;
		}

		private static bool AppendTranslatedTextSegment(string segment, StringBuilder result, ref bool translatedAnySegment)
		{
			return false;
		}

		private static bool TryGetCachedTranslation(string text, out string translated)
		{
			translated = null;
			return false;
		}

		private static bool ContainsLetterOrDigit(string text)
		{
			return false;
		}

		private static void TrackMiss(string text)
		{
		}

		public static bool HasTranslation(string text, out string translation)
		{
			translation = null;
			return false;
		}

		private static bool IsIgnoreText(string text)
		{
			return false;
		}

		private static bool IsTaggedNumber(string text)
		{
			return false;
		}

		private static bool IsNumberPair(string text)
		{
			return false;
		}

		private static bool IsCountryFirstLastName(string text)
		{
			return false;
		}

		private static void EnsureNameLookupLoaded()
		{
		}

		private static void AddNamesToLookup(List<DataName> source, HashSet<string> target)
		{
		}

		private static bool TryExtractFirstAndLastName(string text, out string firstName, out string lastName)
		{
			firstName = null;
			lastName = null;
			return false;
		}

		private static bool IsPossibleNameToken(string text)
		{
			return false;
		}

		private static bool IsNumber(string text)
		{
			return false;
		}

		private static bool IsTemperature(string text)
		{
			return false;
		}

		public static void AddTranslation(string originalText, string translatedText)
		{
		}

		public static void TryAddTranslation(string originalText, string translatedText)
		{
		}

		public static bool TryGetTranslation(string originalText, out string translatedText)
		{
			translatedText = null;
			return false;
		}

		public static List<string> GetTranslationList(List<string> list)
		{
			return null;
		}

		public static void ResetCache()
		{
		}

		public static void SaveCache(string language)
		{
		}

		public static void SaveENMissCache()
		{
		}

		public static void LoadCache(string language)
		{
		}

		public static void LoadDomain(string language, string domainName)
		{
		}
	}
}
