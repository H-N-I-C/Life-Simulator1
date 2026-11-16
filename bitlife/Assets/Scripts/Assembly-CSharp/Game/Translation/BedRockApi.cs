using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine.Events;

namespace Game.Translation
{
	public class BedRockApi
	{
		private class BedRockNameTagContext
		{
			public readonly string HeroFirstName;

			public readonly string HeroFullName;

			public readonly string NpcFirstName;

			public readonly string NpcFullName;

			public BedRockNameTagContext(SimPersonName heroName, SimPersonName npcName)
			{
			}
		}

		private struct NameTagReplacement
		{
			public readonly string Name;

			public readonly string Tag;

			public NameTagReplacement(string name, string tag)
			{
				Name = null;
				Tag = null;
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<TranslationServerResultItem, bool> _003C_003E9__8_4;

			public static Func<TranslationServerResultItem, string> _003C_003E9__8_5;

			public static Func<IGrouping<string, TranslationServerResultItem>, string> _003C_003E9__8_6;

			public static Func<IGrouping<string, TranslationServerResultItem>, TranslationServerResultItem> _003C_003E9__8_7;

			public static Func<TranslationEntry, bool> _003C_003E9__8_2;

			public static Func<NameTagReplacement, string> _003C_003E9__17_0;

			public static Func<NameTagReplacement, string> _003C_003E9__17_5;

			public static Func<IGrouping<string, NameTagReplacement>, bool> _003C_003E9__17_1;

			public static Func<IGrouping<string, NameTagReplacement>, string> _003C_003E9__17_2;

			public static Func<NameTagReplacement, int> _003C_003E9__17_4;

			internal bool _003CCallBedrockAPI_003Eb__8_4(TranslationServerResultItem e)
			{
				return false;
			}

			internal string _003CCallBedrockAPI_003Eb__8_5(TranslationServerResultItem e)
			{
				return null;
			}

			internal string _003CCallBedrockAPI_003Eb__8_6(IGrouping<string, TranslationServerResultItem> g)
			{
				return null;
			}

			internal TranslationServerResultItem _003CCallBedrockAPI_003Eb__8_7(IGrouping<string, TranslationServerResultItem> g)
			{
				return null;
			}

			internal bool _003CCallBedrockAPI_003Eb__8_2(TranslationEntry entry)
			{
				return false;
			}

			internal string _003CGetNameTagReplacements_003Eb__17_0(NameTagReplacement candidate)
			{
				return null;
			}

			internal bool _003CGetNameTagReplacements_003Eb__17_1(IGrouping<string, NameTagReplacement> group)
			{
				return false;
			}

			internal string _003CGetNameTagReplacements_003Eb__17_5(NameTagReplacement candidate)
			{
				return null;
			}

			internal string _003CGetNameTagReplacements_003Eb__17_2(IGrouping<string, NameTagReplacement> group)
			{
				return null;
			}

			internal int _003CGetNameTagReplacements_003Eb__17_4(NameTagReplacement candidate)
			{
				return 0;
			}
		}

		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public HashSet<string> ambiguousNames;

			internal bool _003CGetNameTagReplacements_003Eb__3(NameTagReplacement candidate)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public BedRockNameTagContext nameTagContext;

			public TranslationRequestContext context;

			public TranslationInitModel gameSettings;

			public int retryCount;

			public List<TranslationEntry> text;

			public string language;

			public Action<List<TranslationEntry>, bool, long> onLoad;

			public SimPerson focusPerson;

			public string targetLang;

			public Action _003C_003E9__3;

			internal TranslationServerEntry _003CCallBedrockAPI_003Eb__0(TranslationEntry x)
			{
				return null;
			}

			internal void _003CCallBedrockAPI_003Eb__1(string result, HttpStatusCode status)
			{
			}

			internal void _003CCallBedrockAPI_003Eb__3()
			{
			}
		}

		private static string _bedrockUrl;

		private const string HeroFirstNameTag = "<hero-first-name>";

		private const string HeroNameTag = "<hero-name>";

		private const string NpcFirstNameTag = "<npc-first-name>";

		private const string NpcNameTag = "<npc-name>";

		private const string NameBoundaryPattern = "[\\p{L}\\p{M}\\p{N}_]";

		public static readonly UnityEvent<TranslationServerResult> OnBedRockTranslationResponse;

		private static string ToLanguageCode(string language)
		{
			return null;
		}

		public static void CallBedrockAPI(List<TranslationEntry> text, string language, Action<List<TranslationEntry>, bool, long> onLoad, int retryCount = 0, SimPerson focusPerson = null, TranslationRequestContext context = null)
		{
		}

		public static void Init(string modelBedRockUrl)
		{
		}

		public static string FormatOriginalForBedrockRequest(string original, SimPersonName heroName = null, SimPersonName npcName = null)
		{
			return null;
		}

		public static string ApplyNameTags(string text, SimPersonName heroName = null, SimPersonName npcName = null)
		{
			return null;
		}

		public static string RestoreNameTags(string text, SimPersonName heroName = null, SimPersonName npcName = null)
		{
			return null;
		}

		private static string FormatOriginalForBedrockRequest(string original, BedRockNameTagContext context)
		{
			return null;
		}

		private static string ApplyNameTags(string text, BedRockNameTagContext context)
		{
			return null;
		}

		private static string RestoreNameTags(string text, BedRockNameTagContext context)
		{
			return null;
		}

		private static void RestoreNameTags(TranslationServerResult results, BedRockNameTagContext context)
		{
		}

		private static IEnumerable<NameTagReplacement> GetNameTagReplacements(BedRockNameTagContext context)
		{
			return null;
		}

		private static void AddNameTagReplacement(List<NameTagReplacement> candidates, string name, string tag)
		{
		}

		private static string ReplaceName(string text, string name, string tag)
		{
			return null;
		}

		private static string RestoreNameTag(string text, string tag, string name)
		{
			return null;
		}

		private static string GetNormalizedName(string name)
		{
			return null;
		}
	}
}
