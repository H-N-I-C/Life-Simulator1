using System.Collections;
using System.Collections.Generic;

public class MiniGameDataPoliticalAgenda
{
	private int MaxHours__BackingField;

	private Dictionary<string, string> PreDictionary__BackingField;

	private ArrayList PreOrder__BackingField;

	private Dictionary<string, string> PostDictionary__BackingField;

	public int MaxHours
	{
		get
		{
			return MaxHours__BackingField;
		}
		set
		{
			MaxHours__BackingField = value;
		}
	}

	public Dictionary<string, string> PreDictionary
	{
		get
		{
			return PreDictionary__BackingField;
		}
		set
		{
			PreDictionary__BackingField = value;
		}
	}

	public ArrayList PreOrder
	{
		get
		{
			return PreOrder__BackingField;
		}
		set
		{
			PreOrder__BackingField = value;
		}
	}

	public Dictionary<string, string> PostDictionary
	{
		get
		{
			return PostDictionary__BackingField;
		}
		set
		{
			PostDictionary__BackingField = value;
		}
	}
}
