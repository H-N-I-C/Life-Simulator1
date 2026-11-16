using System;
using System.IO;
using Backend.Authentication;
using UnityEngine;

public static class AgeUp
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<GetConfigResponse> _003C_003E9__2_0;

		public static Func<IPushScenarioConfigEntry, int> _003C_003E9__5_0;

		internal void _003CSetup_003Eb__2_0(GetConfigResponse settings)
		{
		}

		internal int _003CGetNextPushScenarios_003Eb__5_0(IPushScenarioConfigEntry x)
		{
			return 0;
		}
	}

	private const string PUSH_SCENARIO_CONFIG_PREVIOUS = "Previous_Push_Hash";

	private static PushScenarioCollection _pushScenarios;

	private static void Setup()
	{
	}

	public static bool IsActive()
	{
		return false;
	}

	public static bool IsFirstInChain()
	{
		return false;
	}

	public static Scenario[] GetNextPushScenarios()
	{
		return null;
	}

	public static void SetConfig(IPushScenarioConfig config)
	{
	}

	public static IPushScenarioConfig GetConfig()
	{
		return null;
	}

	public static IPushScenarioConfig LoadLastConfig()
	{
		return null;
	}

	public static void SaveCurrentConfig()
	{
	}

	private static bool IsInAgeRange(IPushScenarioConfigEntry entry)
	{
		return false;
	}

	private static bool TryDrawContent(IPushScenarioConfigEntry entry, out IPushScenarioContent[] contents)
	{
		contents = null;
		return false;
	}

	private static bool RequirementMet(IPushScenarioContent content)
	{
		return false;
	}

	private static bool HasShownThisLife(IPushScenarioContent scenarioContent)
	{
		return false;
	}

	private static void DeleteConfigFile()
	{
	}

	private static void SaveConfig(IPushScenarioConfig config)
	{
	}

	private static bool TryGetPreviousConfigFile(out FileInfo previousConfigFile)
	{
		previousConfigFile = null;
		return false;
	}

	private static string ConstructFilePath(string hashString)
	{
		return null;
	}

	private static Hash128 CalculateConfigHash(string configJson)
	{
		return default;
	}

	private static Hash128 CalculateConfigHash(IPushScenarioConfig config)
	{
		return default;
	}

	private static string SerializeConfig(IPushScenarioConfig config)
	{
		return null;
	}
}
