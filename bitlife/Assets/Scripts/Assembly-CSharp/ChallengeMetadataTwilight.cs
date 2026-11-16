using System.Collections;

public class ChallengeMetadataTwilight : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsCliqueRequirement()
	{
		return false;
	}

	private bool MeetsMarryAgeRequirement()
	{
		return false;
	}

	public bool QuestMeetsMarryAgeRequirement()
	{
		return false;
	}

	public SimLover GetQuestApplicableLover()
	{
		return null;
	}

	public SimLover GetApplicableLover()
	{
		return null;
	}

	private bool MeetsChildRequirement()
	{
		return false;
	}
}
