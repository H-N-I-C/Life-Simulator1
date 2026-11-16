using System;

[Serializable]
public class SimMusicInstrument
{
	[NonSerialized]
	public DataMusicInstrument _Data;

	private string InstrumentName__BackingField;

	private int NumLessons__BackingField;

	private int AgeAtLastPractice__BackingField;

	private int NumPracticesAtCurrentAge__BackingField;

	private float Att_skill__BackingField;

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

	public string InstrumentName
	{
		get
		{
			return InstrumentName__BackingField;
		}
		set
		{
			InstrumentName__BackingField = value;
		}
	}

	public int NumLessons
	{
		get
		{
			return NumLessons__BackingField;
		}
		set
		{
			NumLessons__BackingField = value;
		}
	}

	public int AgeAtLastPractice
	{
		get
		{
			return AgeAtLastPractice__BackingField;
		}
		set
		{
			AgeAtLastPractice__BackingField = value;
		}
	}

	public int NumPracticesAtCurrentAge
	{
		get
		{
			return NumPracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumPracticesAtCurrentAge__BackingField = value;
		}
	}

	public float Att_skill
	{
		get
		{
			return Att_skill__BackingField;
		}
		set
		{
			Att_skill__BackingField = value;
		}
	}

	public SimMusicInstrument(DataMusicInstrument theData)
	{
	}

	public void ImpactSkill(int impactScore)
	{
	}
}
