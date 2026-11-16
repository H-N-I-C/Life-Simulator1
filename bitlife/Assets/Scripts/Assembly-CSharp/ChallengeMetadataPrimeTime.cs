using System.Collections;

public class ChallengeMetadataPrimeTime : ChallengeMetadata
{
	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	public bool MeetsProAthleteRequirement()
	{
		return false;
	}

	private bool MeetsChampionshipRequirement()
	{
		return false;
	}

	private bool MeetsEarningsRequirement()
	{
		return false;
	}
}
