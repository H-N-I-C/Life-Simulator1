using System;
using System.Collections.Generic;

[Serializable]
public class SimHustle
{
	private const float MIN_HUSTLE_CONFIG_SCORE = 4f;

	private const float MAX_HUSTLE_CONFIG_SCORE = 20f;

	private const float HUSTLE_CONFIG_SCORE_MULTIPLIER = 5f;

	public const string MISSING_INSTRUMENT_NAME = "---";

	private int HustleType__BackingField;

	private float SuccessScore__BackingField;

	private float TotalEarnings__BackingField;

	private float CollectionBoxCash__BackingField;

	private List<string> CollectionBoxItems__BackingField;

	private int YearsInHustle__BackingField;

	private SimHustleConfiguration Configuration__BackingField;

	private bool CollectionBoxStolenInSession__BackingField;

	public const string BUSKING_INVALID_NO_INSTRUMENT = "no_instrument";

	public const string BUSKING_INVALID_NO_MUSIC_TYPE = "no_music_type";

	public const string BUSKING_INVALID_NO_ATTIRE = "no_attire";

	public int HustleType
	{
		get
		{
			return HustleType__BackingField;
		}
		set
		{
			HustleType__BackingField = value;
		}
	}

	public float SuccessScore
	{
		get
		{
			return SuccessScore__BackingField;
		}
		set
		{
			SuccessScore__BackingField = value;
		}
	}

	public float TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	public float CollectionBoxCash
	{
		get
		{
			return CollectionBoxCash__BackingField;
		}
		set
		{
			CollectionBoxCash__BackingField = value;
		}
	}

	public List<string> CollectionBoxItems
	{
		get
		{
			return CollectionBoxItems__BackingField;
		}
		set
		{
			CollectionBoxItems__BackingField = value;
		}
	}

	public int YearsInHustle
	{
		get
		{
			return YearsInHustle__BackingField;
		}
		set
		{
			YearsInHustle__BackingField = value;
		}
	}

	public SimHustleConfiguration Configuration
	{
		get
		{
			return Configuration__BackingField;
		}
		set
		{
			Configuration__BackingField = value;
		}
	}

	public bool CollectionBoxStolenInSession
	{
		get
		{
			return CollectionBoxStolenInSession__BackingField;
		}
		set
		{
			CollectionBoxStolenInSession__BackingField = value;
		}
	}

	public SimHustle(int hustleType, SimHustleConfiguration theConfig = null)
	{
	}

	public float StartingPanhandlerConfigScore()
	{
		return 0f;
	}

	public void ImpactSuccessScore(int impactScore)
	{
	}

	public string GetCurrentPropString()
	{
		return null;
	}

	public string GetCurrentSignString()
	{
		return null;
	}

	public DataHustlerPanhandleStrategy GetCurrentPanhandleStrategy()
	{
		return null;
	}

	public SimMusicInstrumentPhysical GetCurrentBuskingInstrument()
	{
		return null;
	}

	public SimMusicInstrument GetCurrentBuskingInstrumentKnowledge()
	{
		return null;
	}

	public bool IsBuskingConfigurationValid()
	{
		return false;
	}

	public bool IsBuskingConfigurationValid(out SimMusicInstrumentPhysical currentInstrument)
	{
		currentInstrument = null;
		return false;
	}

	private bool HasResolvableMusicType()
	{
		return false;
	}

	private bool HasResolvableAttire()
	{
		return false;
	}

	public string GetBuskingConfigurationInvalidReason()
	{
		return null;
	}

	public float StartingBuskerConfigScore()
	{
		return 0f;
	}

	public string GetCurrentBuskingInstrumentName(bool singular = false)
	{
		return null;
	}

	public string GetCurrentMusicType()
	{
		return null;
	}

	public string GetCurrentHustlerAttire()
	{
		return null;
	}

	public float StartingPerformerConfigScore()
	{
		return 0f;
	}

	public static string GetPerformActForIndex(int index)
	{
		return null;
	}

	public static int GetIndexForActName(string actName)
	{
		return 0;
	}

	public DataHustlerPerformAct GetCurrentPerformingAct()
	{
		return null;
	}

	public int GetCurrentPerformingActIndex()
	{
		return 0;
	}

	public int GetWeeksMissed()
	{
		return 0;
	}

	public void SetWeeksMissed(int weeks)
	{
	}

	public float StartingScammerConfigScore()
	{
		return 0f;
	}

	public DataHustlerScamType GetCurrentScam()
	{
		return null;
	}

	public int GetCurrentScamTypeIndex()
	{
		return 0;
	}

	public string CurrentCollectionBoxName()
	{
		return null;
	}

	public void SetNewCollectionBox()
	{
	}

	public void AdjustCashInCollectionBox(float value)
	{
	}

	public void AdjustTotalEarnings(float value)
	{
	}
}
