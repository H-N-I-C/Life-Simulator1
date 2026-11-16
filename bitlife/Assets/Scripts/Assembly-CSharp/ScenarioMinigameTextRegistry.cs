using System.Collections;
using System.Collections.Generic;

public class ScenarioMinigameTextRegistry : IEnumerable<KeyValuePair<string, ScenarioMiniGameTextEntry>>, IEnumerable
{
	private readonly Dictionary<string, ScenarioMiniGameTextEntry> mEntries;

	// C# has no syntax for parameterized property 'Item'.
	public ScenarioMiniGameTextEntry get_Item(string key)
	{
		return null;
	}

	public int Count => 0;

	public IEnumerable<string> Keys => null;

	public IEnumerable<ScenarioMiniGameTextEntry> Values => null;

	public ScenarioMinigameTextRegistry()
	{
	}

	public ScenarioMinigameTextRegistry(IEnumerable<KeyValuePair<string, ScenarioMiniGameTextEntry>> initialEntries)
	{
	}

	public void Add(string key, ScenarioMiniGameTextEntry entry)
	{
	}

	public void Add(string key, string entryTitle)
	{
	}

	public IEnumerator<KeyValuePair<string, ScenarioMiniGameTextEntry>> GetEnumerator()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public bool TryGetEntry(string key, out ScenarioMiniGameTextEntry entry)
	{
		entry = null;
		return false;
	}

	public bool ContainsKey(string pastVerb)
	{
		return false;
	}

	public void Remove(string key)
	{
	}
}
