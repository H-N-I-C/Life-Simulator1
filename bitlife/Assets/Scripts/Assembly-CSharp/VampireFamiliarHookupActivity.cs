public class VampireFamiliarHookupActivity
{
	private SimVampireFamiliar Familiar__BackingField;

	private bool Accepted__BackingField;

	private bool SpouseWillArgue__BackingField;

	private int FamiliarEnjoyment__BackingField;

	private int HeroEnjoyment__BackingField;

	private readonly BracketParser _parser;

	private bool _heroIsCheating;

	public SimVampireFamiliar Familiar
	{
		get
		{
			return Familiar__BackingField;
		}
		private set
		{
			Familiar__BackingField = value;
		}
	}

	public bool Accepted
	{
		get
		{
			return Accepted__BackingField;
		}
		private set
		{
			Accepted__BackingField = value;
		}
	}

	public bool SpouseWillArgue
	{
		get
		{
			return SpouseWillArgue__BackingField;
		}
		private set
		{
			SpouseWillArgue__BackingField = value;
		}
	}

	public int FamiliarEnjoyment
	{
		get
		{
			return FamiliarEnjoyment__BackingField;
		}
		private set
		{
			FamiliarEnjoyment__BackingField = value;
		}
	}

	public int HeroEnjoyment
	{
		get
		{
			return HeroEnjoyment__BackingField;
		}
		private set
		{
			HeroEnjoyment__BackingField = value;
		}
	}

	public VampireFamiliarHookupActivity(SimVampireFamiliar familiar)
	{
	}

	public string GetLoverSexualityString()
	{
		return null;
	}

	public void AskToHookup()
	{
	}

	public VampireFamiliarActivity GetFamiliarActivity()
	{
		return null;
	}

	public bool ShouldSkipSafeSexScenario()
	{
		return false;
	}

	private void UpdateHookupChallengeData()
	{
	}

	public void DoHookup()
	{
	}

	public Scenario ShouldPushCheaterScavengerHuntScenario()
	{
		return null;
	}
}
