using System;
using Game;

[Serializable]
public class SimOutdoorCatchableFish
{
	[NonSerialized]
	private DataOutdoorCatchableFish _fishData;

	private string Species__BackingField;

	private string Name__BackingField;

	private float Weight__BackingField;

	private int Age__BackingField;

	private GENDER Gender__BackingField;

	private int Value__BackingField;

	public string Species
	{
		get
		{
			return Species__BackingField;
		}
		private set
		{
			Species__BackingField = value;
		}
	}

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		private set
		{
			Name__BackingField = value;
		}
	}

	public float Weight
	{
		get
		{
			return Weight__BackingField;
		}
		private set
		{
			Weight__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		private set
		{
			Age__BackingField = value;
		}
	}

	public GENDER Gender
	{
		get
		{
			return Gender__BackingField;
		}
		private set
		{
			Gender__BackingField = value;
		}
	}

	public int Value
	{
		get
		{
			return Value__BackingField;
		}
		private set
		{
			Value__BackingField = value;
		}
	}

	public DataOutdoorCatchableFish Fish => null;

	public SimOutdoorCatchableFish(DataOutdoorCatchableFish fish)
	{
	}

	public string EmojiString()
	{
		return null;
	}

	public string Relationship()
	{
		return null;
	}

	public string PronounStringForType(int type)
	{
		return null;
	}

	public string PronounStringForType(PRONOUN_TYPE pronounType)
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	private int GenerateFishValue()
	{
		return 0;
	}

	private float GenerateFishWeight()
	{
		return 0f;
	}
}
