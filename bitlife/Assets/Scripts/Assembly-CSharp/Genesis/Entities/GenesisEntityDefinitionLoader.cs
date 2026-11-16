using System;
using System.Collections.Generic;

namespace Genesis.Entities
{
	public static class GenesisEntityDefinitionLoader
	{
		private sealed class _003C_003Ec__DisplayClass41_0
		{
			public GenesisEntityDefinition def;

			public int lineNumber;

			internal void _003CParseRow_003Eb__0(string key, string value)
			{
			}
		}

		private const string CsvResourceName = "genesis_entity_defs";

		private const string SubtypesResourceName = "genesis_npc_subtypes";

		private const string ObjectSubtypesResourceName = "genesis_object_subtypes";

		private const string CsvFileName = "genesis_entity_defs.csv";

		private const string SubtypesCsvFileName = "genesis_npc_subtypes.csv";

		private const string ObjectSubtypesCsvFileName = "genesis_object_subtypes.csv";

		private const int ColEntityType = 0;

		private const int ColKind = 1;

		private const int ColAutoFillMin = 2;

		private const int ColDeathScenarioFile = 3;

		private const int ColDeadRetentionYears = 4;

		private const int ColMinAge = 5;

		private const int ColMaxAge = 6;

		private const int ColDefaultProperties = 7;

		private const int ColGenderWeights = 8;

		private const int ColLocationFollowsHero = 9;

		private const int ColDisplayStringUpper = 10;

		private const int ColDisplayStringLower = 11;

		private const int ColNamePoolFemale = 12;

		private const int ColNamePoolMale = 13;

		private const int ColNamePoolOther = 14;

		private const int ColSkinToneFemale = 15;

		private const int ColSkinToneMale = 16;

		private const int ColSkinToneOther = 17;

		private const int ColMaritalStatusWeights = 18;

		private const int ColNumChildrenWeights = 19;

		private const int ColAgeUpResetProperties = 20;

		private const int ColGodModeProperties = 21;

		private const int MinColumnCount = 8;

		private static List<GenesisEntityDefinition> _definitions;

		private static bool _loaded;

		private const int SubColNpcId = 0;

		private const int SubColSubtype = 1;

		private const int SubColWeight = 2;

		private const int SubColHeroRelType = 3;

		private const int SubColHeroRelMin = 4;

		private const int SubColHeroRelMax = 5;

		private const int SubColDeathScenarioId = 6;

		private const int SubColStandardAttributes = 7;

		private const int SubColPropertiesBinary = 8;

		private const int SubColPropertiesString = 9;

		private const int SubColPropertiesNumeric = 10;

		private const int SubColAgeUpHeroRelationship = 11;

		private const int SubColAgeUpStandardAttributes = 12;

		private const int SubColAgeUpPropertiesNumeric = 13;

		private const int SubMinColumnCount = 11;

		public static void LoadDefinitions(bool forceReload = false)
		{
		}

		private static string LoadCsvText(string resourceName, string fileName)
		{
			return null;
		}

		public static void LoadDefinitionsFromText(string csvText)
		{
		}

		public static int LoadSubtypesFromText(string csvText)
		{
			return 0;
		}

		public static int LoadObjectSubtypesFromText(string csvText)
		{
			return 0;
		}

		public static void ApplyToContainer(GenesisEntityContainer container)
		{
		}

		public static GenesisEntityDefinition GetDefinition(string entityType)
		{
			return null;
		}

		public static List<GenesisEntityDefinition> GetAllDefinitions()
		{
			return null;
		}

		public static void Reset()
		{
		}

		private static void ParseCsvText(string csvText)
		{
		}

		private static GenesisEntityDefinition ParseRow(string line, int lineNumber)
		{
			return null;
		}

		private static void SetGodModePropertyIfValid(GenesisEntityDefinition def, string key, string value, int lineNumber)
		{
		}

		public static string[] SplitCsvLine(string line)
		{
			return null;
		}

		internal static void ParseDefaultProperties(GenesisEntityDefinition def, string json, int lineNumber)
		{
		}

		private static void ParseStringProperties(string json, int lineNumber, string columnName, Action<string, string> setProperty)
		{
		}

		private static string ParseJsonString(string text, ref int pos)
		{
			return null;
		}

		internal static void ParseWeightedJson(string json, out List<string> keys, out List<int> weights)
		{
			keys = null;
			weights = null;
		}

		internal static void ParseJsonStringArray(string json, List<string> target)
		{
		}

		private static int ParseSubtypesCsvText(string csvText)
		{
			return 0;
		}

		private static GenesisEntitySubtypeDefinition ParseSubtypeRow(string line, int lineNumber)
		{
			return null;
		}

		internal static void ParseSubtypeAttributes(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static void ParseSubtypeBinaryProperties(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static void ParseSubtypeStringProperties(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static void ParseSubtypeNumericProperties(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static List<KeyValuePair<string, string>> ParseNestedJsonObjects(string json)
		{
			return null;
		}

		internal static int ExtractJsonInt(string json, string key, int fallback)
		{
			return 0;
		}

		internal static string ExtractJsonString(string json, string key, string fallback)
		{
			return null;
		}

		internal static void ParseSubtypeAgeUpHeroRelationship(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static void ParseSubtypeAgeUpAttributes(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}

		internal static void ParseSubtypeAgeUpNumericProperties(GenesisEntitySubtypeDefinition subtype, string json, int lineNumber)
		{
		}
	}
}
