using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game;
using Genesis.Entities;

namespace Genesis.Controllers
{
	public class GenesisMemory
	{
		internal sealed class TransactionSnapshot
		{
			internal readonly Dictionary<string, object> Shared;

			internal readonly Dictionary<string, object> Internal;

			internal readonly Dictionary<string, object> ControllerTransient;

			internal TransactionSnapshot(Dictionary<string, object> shared, Dictionary<string, object> internalValues, Dictionary<string, object> controllerTransient)
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Converter<GenesisContent, string> _003C_003E9__43_3;

			internal string _003CParseGenesisDataSheet_003Eb__43_3(GenesisContent content)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass41_0
		{
			public GenesisMemory _003C_003E4__this;

			public bool regexFound;

			internal string _003CInject_003Eb__0(Match match)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass43_0
		{
			public GenesisContent selectedContent;

			internal string _003CParseGenesisDataSheet_003Eb__0(Match m)
			{
				return null;
			}

			internal string _003CParseGenesisDataSheet_003Eb__1(Match m)
			{
				return null;
			}

			internal string _003CParseGenesisDataSheet_003Eb__2(Match m)
			{
				return null;
			}
		}

		private const string GenesisDataSheetCommandPattern = "genesis\\.data\\.([A-Za-z\\-]+)\\.([A-Za-z]+)\\(([A-Za-z _0-9\\-:]*)\\)";

		public const string ObjectDataFocusMemoryKey = "focus_data";

		private readonly Dictionary<string, object> Data;

		private readonly Dictionary<string, object> DataInternal;

		private readonly Dictionary<string, object> ControllerTransient;

		private readonly ScenarioController _owningScenarioController;

		public GenesisMemory(ScenarioController owningScenarioController, Dictionary<string, object> Source)
		{
		}

		internal TransactionSnapshot CaptureTransactionSnapshot()
		{
			return null;
		}

		internal void RestoreTransactionSnapshot(TransactionSnapshot snapshot)
		{
		}

		public void SetObjectDataFocus(GenesisObjectDataFocus focus)
		{
		}

		public bool TryGetObjectDataFocus(out GenesisObjectDataFocus focus)
		{
			focus = null;
			return false;
		}

		public void ClearObjectDataFocus()
		{
		}

		public void ClearFocusedEntityReference()
		{
		}

		internal void SetControllerTransientValue(string name, object value)
		{
		}

		internal void ClearControllerTransientValue(string name)
		{
		}

		internal bool TryGetControllerTransientValue(string name, out object value)
		{
			value = null;
			return false;
		}

		internal void ClearLegacySharedValue(string name)
		{
		}

		private void SetEntityInternal(string name, object obj)
		{
		}

		public void SetValue(string name, double value)
		{
		}

		public void SetValue(string name, string value)
		{
		}

		public void SetValue(string name, bool value)
		{
		}

		public void SetValue(string name, int value)
		{
		}

		public void SetValue(string name, float value)
		{
		}

		public void SetEntity(string name, SimPerson person)
		{
		}

		public void SetEntity(string name, SimPet pet)
		{
		}

		public void SetEntity(string name, SimZooAnimal zooAnimal)
		{
		}

		public void SetEntity(string name, SimGhost ghost)
		{
		}

		public void SetEntity(string name, SimCar car)
		{
		}

		public void SetEntity(string name, SimHouse house)
		{
		}

		public void SetEntity(string id, GenesisList list)
		{
		}

		public void SetEntity(string id, GenesisSlider slider)
		{
		}

		public void SetEntity(string id, GenesisContent content)
		{
		}

		public void SetEntity(string name, GenesisDataEntity entity)
		{
		}

		public void SetEntity(string name, SimEntity entity)
		{
		}

		public void SetEntity(string name, object entity)
		{
		}

		public void SetEntity(string name, DataWeapon weapon)
		{
		}

		public void SetEntity(string name, DataHitman hitman)
		{
		}

		public void SetEntity(string name, SimSecretAgent agent)
		{
		}

		public void SetEntity(string name, SimDealerStreetPerson dealerStreetPerson)
		{
		}

		public object GetEntity(string name)
		{
			return null;
		}

		public string Inject(string valueContent)
		{
			return null;
		}

		private bool ProcessInject(string key, out string output)
		{
			output = null;
			return false;
		}

		public string ParseGenesisDataSheet(string text)
		{
			return null;
		}

		private GenesisContent ResolveSelectedDataSheetContent(GenesisContent source)
		{
			return null;
		}

		public static bool TryParseGenesisDataSheetCommandForTests(string text, out string sheetName, out string functionName, out string functionParams)
		{
			sheetName = null;
			functionName = null;
			functionParams = null;
			return false;
		}

		private static bool TryParseReflectionMethodCall(string expression, out string methodPath, out object[] arguments)
		{
			methodPath = null;
			arguments = null;
			return false;
		}

		private static int FindReflectionMethodOpenParen(string expression)
		{
			return 0;
		}

		private static object[] ParseReflectionArguments(string rawArguments)
		{
			return null;
		}

		private static List<string> SplitReflectionArguments(string rawArguments)
		{
			return null;
		}

		private static object ParseReflectionArgument(string rawArgument)
		{
			return null;
		}

		private bool ProcessInjectGenesis(object item, out string output, string[] dotSplit, object[] parameters)
		{
			output = null;
			return false;
		}

		private static bool ProcessInjectGeneric(object target, out string output, string attribute, object[] parameters)
		{
			output = null;
			return false;
		}

		public object Process(string processedValueString)
		{
			return null;
		}

		private string CleanupValue(string processedValueString)
		{
			return null;
		}

		public void SetEntity(ScenarioDefineModel define)
		{
		}

		public void SetVariable(ScenarioDefineModel define)
		{
		}

		public Dictionary<string, object> GetData()
		{
			return null;
		}

		private bool IsLifeData(object item)
		{
			return false;
		}

		public static void IncrementAge(Dictionary<string, object> data)
		{
		}

		private static bool IsAutoValue(string itemKey)
		{
			return false;
		}
	}
}
