using System.Collections;

public class ChallengeMetadataOlympicStriker : ChallengeMetadata
{
	private const int MIN_CHAMPIONSHIP_COUNT_REQUIREMENT = 2;

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool WasBornInGreece()
	{
		return false;
	}

	private bool JoinedTheTrackTeamInSecondarySchool()
	{
		return false;
	}

	private bool HasEmigratedToTheUnitedKingdom()
	{
		return false;
	}

	private bool HasBecomeAFamousSoccerPlayer()
	{
		return false;
	}

	private bool HasWon2PlusSoccerChampionships()
	{
		return false;
	}
}
