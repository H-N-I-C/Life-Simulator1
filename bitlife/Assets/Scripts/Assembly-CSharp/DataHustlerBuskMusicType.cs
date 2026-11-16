using System.Collections;

public class DataHustlerBuskMusicType
{
	private string MusicType__BackingField;

	private bool NeedsMusicAppended__BackingField;

	private ArrayList PossibleInstrumentsArray__BackingField;

	private ArrayList BestInstrumentsArray__BackingField;

	private int SuccessScore__BackingField;

	public string MusicType
	{
		get
		{
			return MusicType__BackingField;
		}
		set
		{
			MusicType__BackingField = value;
		}
	}

	public bool NeedsMusicAppended
	{
		get
		{
			return NeedsMusicAppended__BackingField;
		}
		set
		{
			NeedsMusicAppended__BackingField = value;
		}
	}

	public ArrayList PossibleInstrumentsArray
	{
		get
		{
			return PossibleInstrumentsArray__BackingField;
		}
		set
		{
			PossibleInstrumentsArray__BackingField = value;
		}
	}

	public ArrayList BestInstrumentsArray
	{
		get
		{
			return BestInstrumentsArray__BackingField;
		}
		set
		{
			BestInstrumentsArray__BackingField = value;
		}
	}

	public int SuccessScore
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
}
