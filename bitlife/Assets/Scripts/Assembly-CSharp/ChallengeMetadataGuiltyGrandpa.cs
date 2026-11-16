using System;
using System.Collections;

public class ChallengeMetadataGuiltyGrandpa : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, bool> _003C_003E9__6_0;

		internal bool _003CMeetRequirementMaleTexas_003Eb__6_0(string t)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementStealCarsOver80()
	{
		return false;
	}

	private bool MeetRequirementPickPocketsOver70()
	{
		return false;
	}

	private bool MeetRequirementFightPrisonOver60()
	{
		return false;
	}

	private bool MeetRequirementNoCrimesBefore60()
	{
		return false;
	}

	private bool MeetRequirementMaleTexas()
	{
		return false;
	}
}
