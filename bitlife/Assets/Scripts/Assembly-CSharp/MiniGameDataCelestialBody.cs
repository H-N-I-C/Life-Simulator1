using System.Collections.Generic;

public class MiniGameDataCelestialBody
{
	private string EmojiString__BackingField;

	private Dictionary<string, string> TableDictionary__BackingField;

	public string EmojiString
	{
		get
		{
			return EmojiString__BackingField;
		}
		set
		{
			EmojiString__BackingField = value;
		}
	}

	public Dictionary<string, string> TableDictionary
	{
		get
		{
			return TableDictionary__BackingField;
		}
		set
		{
			TableDictionary__BackingField = value;
		}
	}
}
