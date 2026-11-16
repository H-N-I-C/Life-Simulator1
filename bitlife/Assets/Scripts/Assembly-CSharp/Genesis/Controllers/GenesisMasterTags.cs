using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Genesis.Entities;
using Genesis.FeatureContainers;

namespace Genesis.Controllers
{
	public static class GenesisMasterTags
	{
		public class TagDefinition
		{
			public string TagId;

			public string Description;

			public string CacheTagForValue;

			public string CacheTagForFollowUpA;

			public string CacheTagForFollowUpB;

			public string CacheTagForFollowUpC;

			public string CacheTagForFollowUpD;

			public string CacheTagForFollowUpE;

			public string CacheTagCost;
		}

		public class TagValue
		{
			public string TagId;

			public string Value;

			public int Weight;

			public string CategorizationA;

			public string CategorizationB;

			public string CategorizationC;

			public string FollowUpA;

			public string FollowUpB;

			public string FollowUpC;

			public string FollowUpD;

			public string FollowUpE;

			public string JsonCostDropdown;
		}

		public class TagSelection
		{
			public string TagId;

			public string Value;

			public string FollowUpA;

			public string FollowUpB;

			public string FollowUpC;

			public string FollowUpD;

			public string FollowUpE;

			public string JsonCost;

			public string CategorizationA;
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static MatchEvaluator _003C_003E9__17_0;

			public static MatchEvaluator _003C_003E9__28_0;

			public static Func<string, bool> _003C_003E9__48_0;

			public static Func<string, bool> _003C_003E9__48_1;

			public static Func<string, string[]> _003C_003E9__48_2;

			public static Func<string[], bool> _003C_003E9__48_3;

			public static Func<string[], string> _003C_003E9__48_4;

			public static Func<string[], string> _003C_003E9__48_5;

			public static MatchEvaluator _003C_003E9__89_0;

			public static MatchEvaluator _003C_003E9__103_0;

			public static Func<string, IGenesisEntity> _003C_003E9__147_0;

			internal string _003CResolvePickTags_003Eb__17_0(Match match)
			{
				return null;
			}

			internal string _003CResolvePickReferenceTags_003Eb__28_0(Match match)
			{
				return null;
			}

			internal bool _003CResolveGameTagMatch_003Eb__48_0(string t)
			{
				return false;
			}

			internal bool _003CResolveGameTagMatch_003Eb__48_1(string t)
			{
				return false;
			}

			internal string[] _003CResolveGameTagMatch_003Eb__48_2(string t)
			{
				return null;
			}

			internal bool _003CResolveGameTagMatch_003Eb__48_3(string[] t)
			{
				return false;
			}

			internal string _003CResolveGameTagMatch_003Eb__48_4(string[] t)
			{
				return null;
			}

			internal string _003CResolveGameTagMatch_003Eb__48_5(string[] t)
			{
				return null;
			}

			internal string _003CResolveGfcTags_003Eb__89_0(Match match)
			{
				return null;
			}

			internal string _003CResolveCurrentEntityTags_003Eb__103_0(Match match)
			{
				return null;
			}

			internal IGenesisEntity _003CTryHopSlot_003Eb__147_0(string id)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass100_0
		{
			public object npcObj;

			internal string _003CResolveNpcTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass109_0
		{
			public ScenarioController controller;

			internal string _003CResolveObjectDerefTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass124_0
		{
			public ScenarioController controller;

			internal string _003CResolveContainerReadGrammarTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass129_0
		{
			public ScenarioController controller;

			internal string _003CResolveReferenceTagsRespectingCache_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass160_0
		{
			public TagValue selected;

			internal bool _003CExecuteDrawRules_003Eb__0(TagValue candidate)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public string text;

			internal string _003CResolveGrammarTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass33_0
		{
			public ScenarioController controller;

			internal string _003CResolveReferenceTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass36_0
		{
			public ScenarioController controller;

			internal string _003CResolveBareReferenceTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public ScenarioController controller;

			internal string _003CResolveCacheTags_003Eb__0(Match match)
			{
				return null;
			}

			internal string _003CResolveCacheTags_003Eb__1(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass41_0
		{
			public ScenarioController controller;

			internal string _003CResolveCacheNumberTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass45_0
		{
			public ScenarioController controller;

			internal string _003CResolveHeroTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass46_0
		{
			public ScenarioController controller;

			internal string _003CResolveLoverTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass47_0
		{
			public ScenarioController controller;

			internal string _003CResolveComponentsTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass49_0
		{
			public string prefixTag;

			internal bool _003CResolveGameTag_003Eb__0(KeyValuePair<string, string> t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass51_0
		{
			public ScenarioController controller;

			internal string _003CResolveNumberTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_0
		{
			public ScenarioController controller;

			internal string _003CResolveNumberValueTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass58_0
		{
			public ScenarioController controller;

			public MatchEvaluator _003C_003E9__0;

			internal string _003CResolveFunctionTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass79_0
		{
			public string categorizationAFilter;

			internal bool _003CResolveTagInternal_003Eb__0(TagValue v)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass83_0
		{
			public IReadOnlyList<string> selectedPicks;

			internal string _003CBindDataSheetHereRandomPicks_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass85_0
		{
			public ScenarioController controller;

			internal string _003CResolveSpecialTags_003Eb__0(Match match)
			{
				return null;
			}
		}

		private static readonly Dictionary<string, TagDefinition> _definitions;

		private static readonly Dictionary<string, List<TagValue>> _values;

		private static readonly Dictionary<string, string> _gameTags;

		private const string CachePrefix = "__mastertag_cache__";

		private static readonly Regex PickTagRegex;

		private static List<string> _activeLocalPickCapture;

		private static bool _suppressFlowPickCapture;

		private static readonly Dictionary<string, string> _pickReferenceStore;

		private static string _pickCaptureScenarioId;

		private static int _pickCaptureOrdinal;

		private const string PickReferenceProbe = "<pickref:";

		private static readonly Regex PickReferenceTagRegex;

		private static readonly Regex GrammarTagRegex;

		private static readonly Regex ReferenceTagRegex;

		private static readonly Regex BareTagRegex;

		private static readonly Regex CacheReferenceRegex;

		private static readonly Regex CacheShortFormRegex;

		private static readonly Regex CacheNumberRegex;

		private static readonly Regex HeroTagRegex;

		private static readonly Regex LoverTagRegex;

		private static readonly Regex ComponentsTagRegex;

		private static readonly Regex NumberTagRegex;

		private static readonly Regex NumberTagValueRegex;

		private static readonly Regex FunctionTagRegex;

		private const int MaxFunctionDepth = 10;

		private const int MaxMetaTagDepth = 5;

		private static readonly Regex SpecialTagRegex;

		private static readonly Regex HereRandomPickContentRegex;

		private static readonly Regex HereRandomPickTagRegex;

		private static readonly Regex GfcTagRegex;

		private static string _focusedEntityId;

		private static string _focusedContainerId;

		private static string _focusedCollectionName;

		private static readonly Regex CurrentEntityTagRegex;

		private static readonly Regex NpcTagRegex;

		private static readonly Regex ObjectDerefTagRegex;

		private const string FocusedObjectMemoryKey = "entity";

		private const string ObjectDrawCachePrefix = "__object_draw__";

		private static readonly Regex ContainerReadGrammarRegex;

		private const string CandidateRoot = "candidate";

		private const string FeatureRoot = "feature";

		private const string FocusDataRoot = "focus_data";

		private static readonly Regex ObjectDataFocusSelectorRegex;

		private const string CandidateTerminalId = "id";

		private static readonly Dictionary<ScenarioController, string> _owningContainerByController;

		private static readonly object CacheClearedSentinel;

		public static bool HasLoadedReferenceValues => false;

		public static string FeatureRootName => null;

		public static void LoadReference1(string csvContent)
		{
		}

		public static void LoadReference2(string csvContent)
		{
		}

		public static void LoadGameTags(string csvContent, bool clearData)
		{
		}

		public static void Clear()
		{
		}

		public static string ResolveAllTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolvePickTags(string text)
		{
			return null;
		}

		internal static string ResolveAllTagsAndCaptureLocalPicks(string text, ScenarioController controller, out IReadOnlyList<string> selectedPicks)
		{
			selectedPicks = null;
			return null;
		}

		public static void BeginScenarioPickCapture(string scenarioId)
		{
		}

		public static void ClearPickReferences()
		{
		}

		public static bool TryGetPickReference(string scenarioId, int pickNumber, out string value)
		{
			value = null;
			return false;
		}

		private static void RecordPickReference(string chosenValue)
		{
		}

		private static string ResolvePickReferenceTags(string text)
		{
			return null;
		}

		private static string MakePickKey(string scenarioId, int pickNumber)
		{
			return null;
		}

		private static string ResolveGrammarTags(string text)
		{
			return null;
		}

		private static string ResolveReferenceTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static void ClearCachedTagAndDependencies(string tagId, ScenarioController controller)
		{
		}

		private static string ResolveBareReferenceTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveCacheTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveCacheNumberTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveHeroTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveLoverTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveComponentsTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveGameTagMatch(string family, string content, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveGameTag(string family, string key)
		{
			return null;
		}

		private static string ResolveNumberTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveNumberValueTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static int ApplyRounding(int value, string roundingStr)
		{
			return 0;
		}

		private static int RoundUpTo(int value, int multiple)
		{
			return 0;
		}

		private static string ResolveFunctionTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveContainerReadGrammarFunctionArguments(string text, ScenarioController controller)
		{
			return null;
		}

		private static int FindBalancedTagEnd(string text, int startIndex)
		{
			return 0;
		}

		private static string ResolveSingleFunction(Match match, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveCountAliveSlotsFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveScaleFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveClampFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveAttributeImpactFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveWeightedPickFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveChanceFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveAddFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveSubFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveMulFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveDivFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveAbsFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveAvgFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveMaxFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static string ResolveMinFunction(string[] parts, ScenarioController controller, string original)
		{
			return null;
		}

		private static bool TryResolveNumericArg(string arg, ScenarioController controller, out double value)
		{
			value = default;
			return false;
		}

		public static TagSelection ResolveTag(string tagId, ScenarioController controller, string categorizationAFilter = null)
		{
			return null;
		}

		private static TagSelection ResolveTagInternal(string tagId, ScenarioController controller, string categorizationAFilter, int depth)
		{
			return null;
		}

		internal static string BindDataSheetHereRandomPicks(string text, IReadOnlyList<string> selectedPicks)
		{
			return null;
		}

		internal static string ResolveDataSheetCompanionTags(string text, IReadOnlyList<string> selectedPicks, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveSpecialTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveSpecialTag(string tagId, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveRandomFullName(ScenarioController controller)
		{
			return null;
		}

		private static string ResolveGfcTags(string text)
		{
			return null;
		}

		public static void SetFocusedEntity(string entityId, string containerId, string collectionName)
		{
		}

		public static void ClearFocusedEntity()
		{
		}

		public static string GetFocusedEntityId()
		{
			return null;
		}

		public static string GetFocusedContainerId()
		{
			return null;
		}

		public static string GetFocusedCollectionName()
		{
			return null;
		}

		private static string ResolveNpcTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveNpcMaritalStatus(SimPerson person)
		{
			return null;
		}

		private static string ResolveNpcBirthplace(SimPerson person)
		{
			return null;
		}

		private static string ResolveCurrentEntityTags(string text)
		{
			return null;
		}

		private static string ResolveEntityProperty(SimEntity entity, string propertyKey)
		{
			return null;
		}

		private static string EntityNumber(float value)
		{
			return null;
		}

		private static string ResolveObjectDerefTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveObjectDrawVerb(string drawType, ScenarioController controller)
		{
			return null;
		}

		private static IGenesisEntity ResolveObjectRef(string objectRef, ScenarioController controller)
		{
			return null;
		}

		internal static string ReadEntityProperty(IGenesisEntity entity, string propertyKey)
		{
			return null;
		}

		public static void SetOwningContainer(ScenarioController controller, string containerId)
		{
		}

		public static void ClearOwningContainer(ScenarioController controller)
		{
		}

		public static void ClearAllOwningContainers()
		{
		}

		public static string GetOwningContainerId(ScenarioController controller)
		{
			return null;
		}

		private static string ResolveContainerReadGrammarTags(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ReResolveEarlyPassTags(string resolved, string raw, ScenarioController controller)
		{
			return null;
		}

		private static string RunEarlyPassReResolution(string value, ScenarioController controller)
		{
			return null;
		}

		private static bool ContainsLatePassTag(string value)
		{
			return false;
		}

		private static bool ContainsContainerReadGrammarTag(string value)
		{
			return false;
		}

		private static string ResolveReferenceTagsRespectingCache(string text, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveFeatureRead(string path, ScenarioController controller, string raw)
		{
			return null;
		}

		private static string ResolveCandidateRead(string path, ScenarioController controller, string raw)
		{
			return null;
		}

		private static string ResolveAliasRead(GenesisFeatureContainerDefinition def, string path, ScenarioController controller, string raw)
		{
			return null;
		}

		public static bool TrySetObjectDataFocus(string selector, ScenarioController controller)
		{
			return false;
		}

		public static bool TrySetObjectDataFocus(string selector, ScenarioController controller, GenesisDataEntity explicitRootEntity)
		{
			return false;
		}

		public static string ResolveObjectDataFocusValue(string relativePath, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveObjectDataFocusRead(string path, ScenarioController controller, string raw)
		{
			return null;
		}

		private static string ResolveObjectDataFocusRelativeValue(GenesisDataEntity rootEntity, GenesisObjectDataFocus focus, string relativePath)
		{
			return null;
		}

		private static GenesisDataEntity ResolveObjectDataFocusRoot(ScenarioController controller, out GenesisObjectDataFocus focus)
		{
			focus = null;
			return null;
		}

		private static GenesisDataEntity ResolveObjectDataFocusEntityById(string entityId)
		{
			return null;
		}

		private static bool IsObjectDataFocusNamespace(GenesisDataEntity rootEntity, string pathPrefix)
		{
			return false;
		}

		private static string CombineFocusPath(string prefix, string relativePath)
		{
			return null;
		}

		public static string ResolveAliasFilterValue(string alias, string path, ScenarioController controller)
		{
			return null;
		}

		public static string ResolveCandidateFilterValue(string path, ScenarioController controller)
		{
			return null;
		}

		public static string ResolveFeatureFilterValue(string prop, ScenarioController controller)
		{
			return null;
		}

		private static string ResolveEntityPathValue(IGenesisEntity entity, string path)
		{
			return null;
		}

		private static IGenesisEntity ResolvePathToEntity(IGenesisEntity entity, string path)
		{
			return null;
		}

		private static IGenesisEntity TryHopSlot(IGenesisEntity entity, string slotPath)
		{
			return null;
		}

		private static IGenesisEntity TryResolveIdToEntity(IGenesisEntity entity, string idPropertyKey)
		{
			return null;
		}

		private static bool TryReadEntityPropertyExact(IGenesisEntity entity, string key, out string value)
		{
			value = null;
			return false;
		}

		private static bool IsIdentityPropertyKey(string key)
		{
			return false;
		}

		private static bool TrySplitTerminalPronoun(string path, out string corePath, out string maleForm, out string femaleForm)
		{
			corePath = null;
			maleForm = null;
			femaleForm = null;
			return false;
		}

		private static string ResolvePronoun(IGenesisEntity entity, string maleForm, string femaleForm, string raw)
		{
			return null;
		}

		private static bool TryGetEntityIsMale(IGenesisEntity entity, out bool isMale)
		{
			isMale = default;
			return false;
		}

		private static bool TryInterpretGenderString(string gender, out bool isMale)
		{
			isMale = default;
			return false;
		}

		public static IGenesisEntity ResolveSlotHolderEntity(string rootAlias, ScenarioController controller)
		{
			return null;
		}

		public static IGenesisEntity ResolveOwnedSlotHolderEntity(string rootAlias, ScenarioController controller)
		{
			return null;
		}

		public static IGenesisEntity ResolveSlotAssignedEntity(IGenesisEntity holder, string slotPath)
		{
			return null;
		}

		public static List<TagValue> ExecuteDrawRules(string tagId, List<DrawRule> drawRules)
		{
			return null;
		}

		private static bool MatchesCategorization(TagValue value, DrawCondition condition)
		{
			return false;
		}

		public static void CacheSelection(string tagId, TagSelection selection, ScenarioController controller)
		{
		}

		private static TagSelection GetCachedSelection(string tagId, ScenarioController controller)
		{
			return null;
		}

		public static void ClearCachedTag(string tagId, ScenarioController controller)
		{
		}

		public static string CleanTagId(string raw)
		{
			return null;
		}

		private static int ParseWeight(string value)
		{
			return 0;
		}
	}
}
