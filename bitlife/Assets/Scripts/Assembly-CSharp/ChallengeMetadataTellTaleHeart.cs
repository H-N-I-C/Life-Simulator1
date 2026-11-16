using System;
using System.Collections;

public class ChallengeMetadataTellTaleHeart : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, bool> _003C_003E9__6_0;

		internal bool _003CMeetRequirementBornMaleVirginia_003Eb__6_0(string t)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementBecomeNurse()
	{
		return false;
	}

	private bool MeetRequirementMurder60older()
	{
		return false;
	}

	private bool MeetRequirementGuilty()
	{
		return false;
	}

	private bool MeetRequirementAddiction()
	{
		return false;
	}

	private bool MeetRequirementBornMaleVirginia()
	{
		return false;
	}
}
