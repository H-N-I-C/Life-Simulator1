public class ScenarioMiniGameTextEntry
{
	private string Title__BackingField;

	private string TranslatedTitle__BackingField;

	private bool OnlyCache__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public string TranslatedTitle
	{
		get
		{
			return TranslatedTitle__BackingField;
		}
		set
		{
			TranslatedTitle__BackingField = value;
		}
	}

	public bool OnlyCache
	{
		get
		{
			return OnlyCache__BackingField;
		}
		set
		{
			OnlyCache__BackingField = value;
		}
	}

	public ScenarioMiniGameTextEntry(string title, bool onlyCache = false)
	{
	}

	public static implicit operator string(ScenarioMiniGameTextEntry entry)
	{
		return null;
	}

	public string GetTitle()
	{
		return null;
	}

	public override string ToString()
	{
		return null;
	}
}
