using System;

[Serializable]
public class SimMusicInstrumentPhysical
{
	private string Name__BackingField;

	public int Age;

	private string Finish__BackingField;

	private float Condition__BackingField;

	private int AgeAtLastPlay__BackingField;

	private int AgeAtLastPolish__BackingField;

	private int AgeAtLastTune__BackingField;

	private bool GivenByParent__BackingField;

	private float OriginalValue__BackingField;

	[NonSerialized]
	private DataMusicInstrument _Data;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public string Finish
	{
		get
		{
			return Finish__BackingField;
		}
		set
		{
			Finish__BackingField = value;
		}
	}

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public int AgeAtLastPlay
	{
		get
		{
			return AgeAtLastPlay__BackingField;
		}
		set
		{
			AgeAtLastPlay__BackingField = value;
		}
	}

	public int AgeAtLastPolish
	{
		get
		{
			return AgeAtLastPolish__BackingField;
		}
		set
		{
			AgeAtLastPolish__BackingField = value;
		}
	}

	public int AgeAtLastTune
	{
		get
		{
			return AgeAtLastTune__BackingField;
		}
		set
		{
			AgeAtLastTune__BackingField = value;
		}
	}

	public bool GivenByParent
	{
		get
		{
			return GivenByParent__BackingField;
		}
		set
		{
			GivenByParent__BackingField = value;
		}
	}

	private float OriginalValue
	{
		get
		{
			return OriginalValue__BackingField;
		}
		set
		{
			OriginalValue__BackingField = value;
		}
	}

	public DataMusicInstrument Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimMusicInstrumentPhysical()
	{
	}

	public SimMusicInstrumentPhysical(bool isNew, DataMusicInstrument data)
	{
	}

	public float CurrentValue()
	{
		return 0f;
	}

	public void IncrementAge()
	{
	}

	public string GenericDescriptionStringAndCapitalize(bool capitalize, bool singular = false)
	{
		return null;
	}

	public SimMusicInstrument GetKnowledgeRepresentation()
	{
		return null;
	}

	public void ImproveConditionAfterTuneOrPolish()
	{
	}

	private bool _003CGetKnowledgeRepresentation_003Eb__42_0(SimMusicInstrument x)
	{
		return false;
	}

	private bool _003CGetKnowledgeRepresentation_003Eb__42_1(SimMusicInstrument x)
	{
		return false;
	}
}
