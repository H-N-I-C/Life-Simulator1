using System;
using System.Collections.Generic;
using Backend.Authentication;
using Game.Genesis.Components;
using Genesis.GameAdaptor;
using Genesis.Lists.Models;
using Genesis.Lists.Runtime;
using UnityEngine;

namespace Genesis.Controllers
{
	public class GenesisController
	{
		private sealed class AgeUpPlacementResource
		{
			private TextAsset Asset__BackingField;

			private List<string> ScenarioIds__BackingField;

			public TextAsset Asset
			{
				get
				{
					return Asset__BackingField;
				}
				set
				{
					Asset__BackingField = value;
				}
			}

			public List<string> ScenarioIds
			{
				get
				{
					return ScenarioIds__BackingField;
				}
				set
				{
					ScenarioIds__BackingField = value;
				}
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<GetConfigResponse> _003C_003E9__14_0;

			public static Func<string, bool> _003C_003E9__41_0;

			internal void _003CInit_003Eb__14_0(GetConfigResponse settings)
			{
			}

			internal bool _003CEnsureAgeUpPlacementResourcesLoaded_003Eb__41_0(string id)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass41_0
		{
			public string id;

			internal bool _003CEnsureAgeUpPlacementResourcesLoaded_003Eb__1(ScenarioController controller)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass59_0
		{
			public string resourceName;

			public string ref2Url;

			public Action onComplete;

			internal void _003CTryLoadRemoteReferenceCsvs_003Eb__0(string ref1Data)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass59_1
		{
			public string ref1Data;

			public _003C_003Ec__DisplayClass59_0 CS_0024_003C_003E8__locals1;

			internal void _003CTryLoadRemoteReferenceCsvs_003Eb__1(string ref2Data)
			{
			}
		}

		private static GenesisSettings _genesisSettings;

		private static GenesisContentAdaptor _adaptor;

		public static Action OnTimerTick;

		private static TimerComponent _timer;

		private static TextureController _textureController;

		private static List<ScenarioController> _scenarioControllers;

		private static ScenarioController _lastScenarioController;

		private static readonly Dictionary<string, ScenarioController> _buildListControllersByResource;

		private static List<AgeUpPlacementResource> _ageUpPlacementResourceCatalog;

		private static int _totalSessionSeconds;

		private static bool _isSystemReady;

		public static GenesisContentAdaptor Adaptor => null;

		private static void Init()
		{
		}

		public static void ResetScenarios()
		{
		}

		private static void CountTimeThisSession()
		{
		}

		public static void LoadTexture(string id)
		{
		}

		public static bool IsTextureReady(string id)
		{
			return false;
		}

		public static Texture2D GetTexture2D(string id)
		{
			return null;
		}

		public static void LoadResourceAndShow(string resourceName, bool show, bool usesLifeData, Action<string, Scenario> giveItemAction = null)
		{
		}

		public static void LoadAndShowScenario(string csvText, bool show, bool usesLifeData, Action<string, Scenario> giveItemAction = null)
		{
		}

		public static Scenario LoadResourceAsQueuedScenario(string csvFileName, ScenarioController existingController = null)
		{
			return null;
		}

		public static Scenario LoadGfcScenario(string resourceId)
		{
			return null;
		}

		public static List<Scenario> LoadGfcFilteredScenarios(string resourceId)
		{
			return null;
		}

		private static bool GfcFilterFailsToParse(List<GenesisFilterModel> parsedFilters)
		{
			return false;
		}

		private static void InvokeSelectedActions(List<GenesisFilterModel> winnerFilters)
		{
		}

		private static string LoadCsvText(string resourceId)
		{
			return null;
		}

		private static string StripCsvExtension(string resourceId)
		{
			return null;
		}

		private static void AssociateOwningContainer(ScenarioController controller, string resourceId)
		{
		}

		public static bool TryDeriveListOwningContainerId(GenesisListDefinition listDefinition, out string containerId)
		{
			containerId = null;
			return false;
		}

		public static void AssociateListOwningContainer(ScenarioController controller, GenesisListDefinition listDefinition)
		{
		}

		public static Scenario LoadScenario(string id)
		{
			return null;
		}

		public static Scenario LoadScenario(string id, string resourceName, ScenarioController preferredController)
		{
			return null;
		}

		private static ScenarioController FindScenarioController(string id)
		{
			return null;
		}

		public static bool TryGetScenarioRow(string id, string resourceName, ScenarioController preferredController, out string[] row, out bool isDesignerFormat)
		{
			row = null;
			isDesignerFormat = default;
			return false;
		}

		private static ScenarioController ResolveScenarioDataOwner(string id, string resourceName, ScenarioController preferredController)
		{
			return null;
		}

		private static void TransferObjectDataFocus(ScenarioController source, ScenarioController target)
		{
		}

		public static int GetTotalSecondsInSession()
		{
			return 0;
		}

		public static Scenario LoadScenarioForFilter(GENESIS_FILTER filter)
		{
			return null;
		}

		public static List<(AgeUpPlacement, Scenario)> LoadAgeUpPlacementScenarios()
		{
			return null;
		}

		private static void EnsureAgeUpPlacementResourcesLoaded()
		{
		}

		private static void EnsureAgeUpPlacementResourceCatalog()
		{
		}

		private static List<string> FindAgeUpPlacementScenarioIds(string csvText)
		{
			return null;
		}

		public static string GetBasePathForCsv(string id)
		{
			return null;
		}

		public static string GetStringFromMemory(string key)
		{
			return null;
		}

		public static bool HasValueInMemory(string key)
		{
			return false;
		}

		public static float GetFloatFromMemory(string key, float defaultValue = 0f)
		{
			return 0f;
		}

		private static bool TryConvertToFloat(object value, out float result)
		{
			result = default;
			return false;
		}

		public static void Inject(string postProcess, ScenarioController owningController)
		{
		}

		public static bool ShowList(string resourceName)
		{
			return false;
		}

		public static bool ShowList(string resourceName, ScenarioController controller, bool dismissAllFirst = false)
		{
			return false;
		}

		public static bool ShowList(string resourceName, ScenarioController controller, GenesisListInvocationSource invocationSource, bool dismissAllFirst = false)
		{
			return false;
		}

		public static List BuildList(string resourceName)
		{
			return null;
		}

		private static void RegisterScenarioController(ScenarioController controller)
		{
		}

		private static bool TryLoadScenarioResource(string resourceName, ScenarioController controller, bool show, Action<string, Scenario> giveItemAction = null)
		{
			return false;
		}

		public static void EnsureGlobalReferenceTagsLoaded()
		{
		}

		private static void TryLoadReferenceCsvs(string resourceName, ScenarioController controller)
		{
		}

		public static void TryLoadRemoteReferenceCsvs(string resourceName, string ref1Csv, string ref2Csv)
		{
		}

		public static void TryLoadRemoteReferenceCsvs(string resourceName, Action onComplete)
		{
		}

		public static ScenarioController GetCurrentController()
		{
			return null;
		}

		public static string GetGenesisData(string command, bool isBody)
		{
			return null;
		}
	}
}
