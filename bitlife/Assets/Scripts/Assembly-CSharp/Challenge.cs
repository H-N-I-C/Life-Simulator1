public class Challenge
{
	private int ChallengeID__BackingField;

	private int SecondsUntilEndOfChallengeFromLastChallengeRequest__BackingField;

	private int NumCompletions__BackingField;

	private ChallengeMetadata Metadata__BackingField;

	private bool LiveChallenge__BackingField;

	public int ChallengeID
	{
		get
		{
			return ChallengeID__BackingField;
		}
		set
		{
			ChallengeID__BackingField = value;
		}
	}

	public int SecondsUntilEndOfChallengeFromLastChallengeRequest
	{
		get
		{
			return SecondsUntilEndOfChallengeFromLastChallengeRequest__BackingField;
		}
		set
		{
			SecondsUntilEndOfChallengeFromLastChallengeRequest__BackingField = value;
		}
	}

	public int NumCompletions
	{
		get
		{
			return NumCompletions__BackingField;
		}
		set
		{
			NumCompletions__BackingField = value;
		}
	}

	public ChallengeMetadata Metadata
	{
		get
		{
			return Metadata__BackingField;
		}
		set
		{
			Metadata__BackingField = value;
		}
	}

	public bool LiveChallenge
	{
		get
		{
			return LiveChallenge__BackingField;
		}
		set
		{
			LiveChallenge__BackingField = value;
		}
	}

	public int SecondsUntilEnd()
	{
		return 0;
	}
}
