using System.Collections;

public class ChallengeMetadataSaveAHorse : ChallengeMetadata
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string birthCity;

		internal bool _003CMeetRequirementBornInTexas_003Eb__0(string t)
		{
			return false;
		}
	}

	private const int MIN_NUMBER_HORSES_TO_BUY = 5;

	private const int MIN_NUMBER_MEN_TO_HOOK_UP_WITH = 5;

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementHookUpWith5PlusMen()
	{
		return false;
	}

	private bool MeetRequirementBuy5PlusHorses()
	{
		return false;
	}

	private bool MeetRequirementPurchaseAnEquestrianRanch()
	{
		return false;
	}

	private bool MeetRequirementBecomeAVeterinarian()
	{
		return false;
	}

	private bool MeetRequirementBornInTexas()
	{
		return false;
	}
}
