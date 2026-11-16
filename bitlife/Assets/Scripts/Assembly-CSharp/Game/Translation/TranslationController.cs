using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Translation
{
	public class TranslationController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<TranslationEntry, string> _003C_003E9__16_0;

			public static Func<TranslationEntry, string> _003C_003E9__16_2;

			public static Func<IGrouping<string, TranslationEntry>, string> _003C_003E9__16_3;

			public static Func<IGrouping<string, TranslationEntry>, TranslationEntry> _003C_003E9__16_4;

			public static Func<TranslationEntry, bool> _003C_003E9__16_5;

			public static Func<TranslationEntry, string> _003C_003E9__16_6;

			public static Func<CandyText, bool> _003C_003E9__17_1;

			public static Func<CandyText, TranslationEntry> _003C_003E9__17_2;

			public static Func<KeyValuePair<string, ScenarioMiniGameTextEntry>, bool> _003C_003E9__19_0;

			public static Func<KeyValuePair<string, ScenarioMiniGameTextEntry>, TranslationEntry> _003C_003E9__19_1;

			public static Action _003C_003E9__38_0;

			public static Action _003C_003E9__39_0;

			internal string _003CGetTranslatedText_003Eb__16_0(TranslationEntry e)
			{
				return null;
			}

			internal string _003CGetTranslatedText_003Eb__16_2(TranslationEntry e)
			{
				return null;
			}

			internal string _003CGetTranslatedText_003Eb__16_3(IGrouping<string, TranslationEntry> g)
			{
				return null;
			}

			internal TranslationEntry _003CGetTranslatedText_003Eb__16_4(IGrouping<string, TranslationEntry> g)
			{
				return null;
			}

			internal bool _003CGetTranslatedText_003Eb__16_5(TranslationEntry e)
			{
				return false;
			}

			internal string _003CGetTranslatedText_003Eb__16_6(TranslationEntry e)
			{
				return null;
			}

			internal bool _003CGetUIElementTranslation_003Eb__17_1(CandyText t)
			{
				return false;
			}

			internal TranslationEntry _003CGetUIElementTranslation_003Eb__17_2(CandyText t)
			{
				return null;
			}

			internal bool _003CGetScenarioMinigameTextRegistryTranslation_003Eb__19_0(KeyValuePair<string, ScenarioMiniGameTextEntry> item)
			{
				return false;
			}

			internal TranslationEntry _003CGetScenarioMinigameTextRegistryTranslation_003Eb__19_1(KeyValuePair<string, ScenarioMiniGameTextEntry> item)
			{
				return null;
			}

			internal void _003CInitializeRecovery_003Eb__38_0()
			{
			}

			internal void _003CTickRecoveryPresentation_003Eb__39_0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public IDisposable lease;

			public List<TranslationEntry> missing;

			public Action<List<string>> result;

			public List<TranslationEntry> entries;

			internal void _003CGetTranslatedText_003Eb__1(List<TranslationEntry> translated, bool success, long errorCode)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public GameObject dataObject;

			public int generation;

			public Life life;

			public Func<bool> isCurrent;

			public Func<bool> valid;

			public Action<bool> result;

			public List<CandyText> textElements;

			internal bool _003CGetUIElementTranslation_003Eb__0()
			{
				return false;
			}

			internal void _003CGetUIElementTranslation_003Eb__3(List<string> translated)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public string originalText;

			internal bool _003CSetNewData_003Eb__0(TranslationEntry e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public int generation;

			public Action<bool> result;

			public ScenarioMinigameTextRegistry registry;

			internal void _003CGetScenarioMinigameTextRegistryTranslation_003Eb__2(List<string> translated)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public int generation;

			public Func<bool> isCurrent;

			public Action<bool> result;

			public Scenario dataObject;

			internal void _003CGetScenarioTranslation_003Eb__0(List<string> translated)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass24_0
		{
			public Action onComplete;

			internal void _003CTranslateListRoot_003Eb__0(List<string> _)
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		internal struct _003C_003Ec__DisplayClass25_0
		{
			public List<string> targetList;
		}

		private sealed class _003C_003Ec__DisplayClass29_0
		{
			public string overrideSelectedLanguage;

			internal bool _003COpenTranslationConfig_003Eb__0(string item)
			{
				return false;
			}
		}

		private const string MASTER_PREF_KEY = "TRANSLATION_ENABLED";

		private static bool IsMasterEnabled__BackingField;

		private static TranslationConfig _config;

		public static readonly UnityEvent<int> OnCacheHit;

		public static readonly UnityEvent<int> OnCacheMiss;

		public static readonly UnityEvent<string> TranslationMiss;

		public static readonly UnityEvent<List<TranslationEntry>> ServerTranslationComplete;

		private static bool pendingOfflineNotice;

		private static bool pendingServiceNotice;

		private static bool presentingNotice;

		private static bool recoverySubscribed;

		public static bool IsMasterEnabled
		{
			get
			{
				return IsMasterEnabled__BackingField;
			}
			private set
			{
				IsMasterEnabled__BackingField = value;
			}
		}

		public static bool IsTranslationInFlight => false;

		public static bool IsShowingAlert => false;

		public static void InitMasterSwitchFromCache()
		{
		}

		public static void ApplyMasterSwitch(bool enabled, bool cache = true)
		{
		}

		public static void GetTranslatedText(List<TranslationEntry> entries, Action<List<string>> result, bool bypassLoader = false, SimPerson currentNpc = null, Func<bool> isCurrent = null)
		{
		}

		public static void GetUIElementTranslation(GameObject dataObject, Action<bool> result = null, bool useIndicator = true, bool onlyCache = false, Func<bool> isCurrent = null)
		{
		}

		private static void SetNewData(GameObject dataObject, List<CandyText> textElements, List<TranslationEntry> newEntries)
		{
		}

		public static void GetScenarioMinigameTextRegistryTranslation(ScenarioMinigameTextRegistry registry, Action<bool> result)
		{
		}

		public static void GetScenarioTranslation(Scenario dataObject, Action<bool> result, Func<bool> isCurrent = null)
		{
		}

		public static string GetTranslationOrOriginal(string text)
		{
			return null;
		}

		public static string[] GetTranslationOrOriginal(string[] list)
		{
			return null;
		}

		private static void SetNewData(Scenario dataObject)
		{
		}

		public static void TranslateListRoot(List list, bool recursive, Action onComplete, Func<bool> isCurrent = null)
		{
		}

		private static void GetItemsFromListItem(List sourceList, bool recursive, List<string> targetList)
		{
		}

		private static void RepopulateListItems(List sourceList)
		{
		}

		public static void PopulateList(List list)
		{
		}

		public static void GetTranslationConfig()
		{
		}

		public static void OpenTranslationConfig(bool showSystem = false, string overrideSelectedLanguage = "", string overrideSelectedSystem = "", bool isFTUE = false, bool trackPreferredLanguageConfirmation = false)
		{
		}

		public static void LoadDomain(string domainName)
		{
		}

		public static void ClearRecoveryNotice(bool preservePending = false)
		{
		}

		private static void QueueRecoveryNotice(bool offline)
		{
		}

		private static bool CanPresentRecoveryNotice(bool offline)
		{
			return false;
		}

		public static void InitializeRecovery()
		{
		}

		public static void TickRecoveryPresentation()
		{
		}

		public static bool IsUserAllowedToUseTranslations()
		{
			return false;
		}

		public static void TickTranslationButton()
		{
		}

		internal static void _003CGetItemsFromListItem_003Eg__AddNewItem_007C25_0(string text, ref _003C_003Ec__DisplayClass25_0 P_1)
		{
		}
	}
}
