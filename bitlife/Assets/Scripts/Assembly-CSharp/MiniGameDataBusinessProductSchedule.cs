using System.Collections;
using System.Collections.Generic;

public class MiniGameDataBusinessProductSchedule
{
	private Dictionary<string, string> PreDictionary__BackingField;

	private ArrayList PreOrder__BackingField;

	private Dictionary<string, string> PostDictionary__BackingField;

	private SimProduct Product__BackingField;

	private bool IsOnboarding__BackingField;

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

	public SimProduct Product
	{
		get
		{
			return Product__BackingField;
		}
		set
		{
			Product__BackingField = value;
		}
	}

	public bool IsOnboarding
	{
		get
		{
			return IsOnboarding__BackingField;
		}
		set
		{
			IsOnboarding__BackingField = value;
		}
	}
}
