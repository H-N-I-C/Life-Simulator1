using System.Collections;

public class DataMusicRole
{
	private int Role__BackingField;

	private string Name__BackingField;

	private bool Solo__BackingField;

	private bool Band__BackingField;

	private int SoloFameScore__BackingField;

	private int BandFameScore__BackingField;

	private ArrayList InstrumentPrimaryArray__BackingField;

	private ArrayList InstrumentSecondaryArray__BackingField;

	private ArrayList SchoolActivityArray__BackingField;

	public int Role
	{
		get
		{
			return Role__BackingField;
		}
		set
		{
			Role__BackingField = value;
		}
	}

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

	public bool Solo
	{
		get
		{
			return Solo__BackingField;
		}
		set
		{
			Solo__BackingField = value;
		}
	}

	public bool Band
	{
		get
		{
			return Band__BackingField;
		}
		set
		{
			Band__BackingField = value;
		}
	}

	public int SoloFameScore
	{
		get
		{
			return SoloFameScore__BackingField;
		}
		set
		{
			SoloFameScore__BackingField = value;
		}
	}

	public int BandFameScore
	{
		get
		{
			return BandFameScore__BackingField;
		}
		set
		{
			BandFameScore__BackingField = value;
		}
	}

	public ArrayList InstrumentPrimaryArray
	{
		get
		{
			return InstrumentPrimaryArray__BackingField;
		}
		set
		{
			InstrumentPrimaryArray__BackingField = value;
		}
	}

	public ArrayList InstrumentSecondaryArray
	{
		get
		{
			return InstrumentSecondaryArray__BackingField;
		}
		set
		{
			InstrumentSecondaryArray__BackingField = value;
		}
	}

	public ArrayList SchoolActivityArray
	{
		get
		{
			return SchoolActivityArray__BackingField;
		}
		set
		{
			SchoolActivityArray__BackingField = value;
		}
	}
}
