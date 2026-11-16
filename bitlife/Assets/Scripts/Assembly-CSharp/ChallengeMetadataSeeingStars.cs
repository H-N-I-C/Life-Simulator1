using System;
using System.Collections;

public class ChallengeMetadataSeeingStars : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFame, bool> _003C_003E9__4_0;

		internal bool _003CMeetRequirementBecomeFamousSocialite_003Eb__4_0(SimFame x)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementMarryAndMurderRoyal()
	{
		return false;
	}

	private bool MeetRequirementMarryFamousMusician()
	{
		return false;
	}

	private bool MeetRequirementBecomeFamousSocialite()
	{
		return false;
	}

	private bool MeetRequirementHookUpWithFamousActor()
	{
		return false;
	}

	private bool MeetRequirementBornCalifornia()
	{
		return false;
	}

	public static void CheckForHookup(SimLover hookupLover)
	{
	}
}
