using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioMiniGameTextDictionary
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ScenarioMiniGameTextEntry, string> _003C_003E9__2_0;

		public static Func<ScenarioMiniGameTextEntry, string> _003C_003E9__6_0;

		internal string _003Cget_Item_003Eb__2_0(ScenarioMiniGameTextEntry x)
		{
			return null;
		}

		internal string _003CGetFromKeys_003Eb__6_0(ScenarioMiniGameTextEntry x)
		{
			return null;
		}
	}

	private readonly Dictionary<string, List<ScenarioMiniGameTextEntry>> mEntries;

	// C# has no syntax for parameterized property 'Item'.
	public ArrayList get_Item(string key)
	{
		return null;
	}

	public int Count => 0;

	public void Add(string key, ScenarioMiniGameTextEntry entry)
	{
	}

	public ArrayList GetFromKeys(string[] keys)
	{
		return null;
	}
}
