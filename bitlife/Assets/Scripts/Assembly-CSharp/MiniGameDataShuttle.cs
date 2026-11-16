public class MiniGameDataShuttle
{
	private int Difficulty__BackingField;

	private int Outcome__BackingField;

	private int Rank__BackingField;

	private string CrashObject__BackingField;

	private bool TrainingMode__BackingField;

	public int Difficulty
	{
		get
		{
			return Difficulty__BackingField;
		}
		set
		{
			Difficulty__BackingField = value;
		}
	}

	public int Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		set
		{
			Outcome__BackingField = value;
		}
	}

	public int Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public string CrashObject
	{
		get
		{
			return CrashObject__BackingField;
		}
		set
		{
			CrashObject__BackingField = value;
		}
	}

	public bool TrainingMode
	{
		get
		{
			return TrainingMode__BackingField;
		}
		set
		{
			TrainingMode__BackingField = value;
		}
	}
}
