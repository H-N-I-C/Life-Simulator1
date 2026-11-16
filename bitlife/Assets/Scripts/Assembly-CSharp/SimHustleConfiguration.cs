using System;

[Serializable]
public class SimHustleConfiguration
{
	private string ParsedCollectionBoxString__BackingField;

	private string OriginalCollectionBoxString__BackingField;

	private string AttireName__BackingField;

	private string CleanAttireName__BackingField;

	private string RegularPropString__BackingField;

	private SimChild ChildProp__BackingField;

	private SimPet PetProp__BackingField;

	private string CurrentStrategyString__BackingField;

	private string CurrentSignString__BackingField;

	private SimMusicInstrumentPhysical CurrentInstrument__BackingField;

	private string MusicTypeString__BackingField;

	private int CurrentActIndex__BackingField;

	private int WeeksMissed__BackingField;

	private int CurrentScamIndex__BackingField;

	public DataHustlerCollectionBox CurrentCollectionBox
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string ParsedCollectionBoxString
	{
		get
		{
			return ParsedCollectionBoxString__BackingField;
		}
		set
		{
			ParsedCollectionBoxString__BackingField = value;
		}
	}

	private string OriginalCollectionBoxString
	{
		get
		{
			return OriginalCollectionBoxString__BackingField;
		}
		set
		{
			OriginalCollectionBoxString__BackingField = value;
		}
	}

	public DataHustlerAttire CurrentAttire
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string AttireName
	{
		get
		{
			return AttireName__BackingField;
		}
		set
		{
			AttireName__BackingField = value;
		}
	}

	public string CleanAttireName
	{
		get
		{
			return CleanAttireName__BackingField;
		}
		set
		{
			CleanAttireName__BackingField = value;
		}
	}

	public DataHustlerPanhandleProp RegularProp
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private string RegularPropString
	{
		get
		{
			return RegularPropString__BackingField;
		}
		set
		{
			RegularPropString__BackingField = value;
		}
	}

	public SimChild ChildProp
	{
		get
		{
			return ChildProp__BackingField;
		}
		set
		{
			ChildProp__BackingField = value;
		}
	}

	public SimPet PetProp
	{
		get
		{
			return PetProp__BackingField;
		}
		set
		{
			PetProp__BackingField = value;
		}
	}

	public DataHustlerPanhandleStrategy CurrentStrategy
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private string CurrentStrategyString
	{
		get
		{
			return CurrentStrategyString__BackingField;
		}
		set
		{
			CurrentStrategyString__BackingField = value;
		}
	}

	public DataHustlerPanhandleSign CurrentSign
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private string CurrentSignString
	{
		get
		{
			return CurrentSignString__BackingField;
		}
		set
		{
			CurrentSignString__BackingField = value;
		}
	}

	public SimMusicInstrumentPhysical CurrentInstrument
	{
		get
		{
			return CurrentInstrument__BackingField;
		}
		set
		{
			CurrentInstrument__BackingField = value;
		}
	}

	public DataHustlerBuskMusicType CurrentMusicType
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string MusicTypeString
	{
		get
		{
			return MusicTypeString__BackingField;
		}
		set
		{
			MusicTypeString__BackingField = value;
		}
	}

	public DataHustlerPerformAct CurrentAct
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int CurrentActIndex
	{
		get
		{
			return CurrentActIndex__BackingField;
		}
		set
		{
			CurrentActIndex__BackingField = value;
		}
	}

	public int WeeksMissed
	{
		get
		{
			return WeeksMissed__BackingField;
		}
		set
		{
			WeeksMissed__BackingField = value;
		}
	}

	public DataHustlerScamType CurrentScam
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int CurrentScamIndex
	{
		get
		{
			return CurrentScamIndex__BackingField;
		}
		set
		{
			CurrentScamIndex__BackingField = value;
		}
	}
}
