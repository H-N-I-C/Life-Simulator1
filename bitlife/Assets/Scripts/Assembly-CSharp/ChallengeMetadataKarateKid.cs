using System.Collections;

public class ChallengeMetadataKarateKid : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool BornMaleInJersey(SimHero hero)
	{
		return false;
	}

	private bool LearnKarateTechniqueInHighSchool(Apollo16ChallengeTracker tracker)
	{
		return false;
	}

	private bool FightABully(Apollo16ChallengeTracker tracker)
	{
		return false;
	}

	private bool DateAPopularGirlInHighSchool(Life life, Apollo16ChallengeTracker tracker)
	{
		return false;
	}

	private bool ObtainBlackBeltAfterHighSchool(Apollo16ChallengeTracker tracker)
	{
		return false;
	}

	public static bool IsHighSchoolStudent(Life life)
	{
		return false;
	}
}
