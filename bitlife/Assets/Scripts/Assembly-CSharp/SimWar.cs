using System;

[Serializable]
public class SimWar
{
	private string BelligerentCountryACode__BackingField;

	private string BelligerentCountryBCode__BackingField;

	[NonSerialized]
	public DataCountry _BelligerentCountryA;

	[NonSerialized]
	public DataCountry _BelligerentCountryB;

	private SimPersonName BelligerentLeaderA__BackingField;

	private SimPersonName BelligerentLeaderB__BackingField;

	private int Age__BackingField;

	private int Stage__BackingField;

	public string BelligerentCountryACode
	{
		get
		{
			return BelligerentCountryACode__BackingField;
		}
		set
		{
			BelligerentCountryACode__BackingField = value;
		}
	}

	public string BelligerentCountryBCode
	{
		get
		{
			return BelligerentCountryBCode__BackingField;
		}
		set
		{
			BelligerentCountryBCode__BackingField = value;
		}
	}

	public DataCountry BelligerentCountryA
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public DataCountry BelligerentCountryB
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimPersonName BelligerentLeaderA
	{
		get
		{
			return BelligerentLeaderA__BackingField;
		}
		set
		{
			BelligerentLeaderA__BackingField = value;
		}
	}

	public SimPersonName BelligerentLeaderB
	{
		get
		{
			return BelligerentLeaderB__BackingField;
		}
		set
		{
			BelligerentLeaderB__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public int Stage
	{
		get
		{
			return Stage__BackingField;
		}
		set
		{
			Stage__BackingField = value;
		}
	}

	private SimWar()
	{
	}

	public SimWar(bool initHere)
	{
	}

	public string GetStringForStage()
	{
		return null;
	}

	public DataCountry GetRandomThirdCountry()
	{
		return null;
	}
}
