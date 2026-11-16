public class MiniGameDataPrisonRiot
{
	private int GridSize__BackingField;

	private int PrisonersNeeded__BackingField;

	private int Guards__BackingField;

	private int Outcome__BackingField;

	private bool AtLeastOneGuardAppeared__BackingField;

	public int GridSize
	{
		get
		{
			return GridSize__BackingField;
		}
		set
		{
			GridSize__BackingField = value;
		}
	}

	public int PrisonersNeeded
	{
		get
		{
			return PrisonersNeeded__BackingField;
		}
		set
		{
			PrisonersNeeded__BackingField = value;
		}
	}

	public int Guards
	{
		get
		{
			return Guards__BackingField;
		}
		set
		{
			Guards__BackingField = value;
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

	public bool AtLeastOneGuardAppeared
	{
		get
		{
			return AtLeastOneGuardAppeared__BackingField;
		}
		set
		{
			AtLeastOneGuardAppeared__BackingField = value;
		}
	}
}
