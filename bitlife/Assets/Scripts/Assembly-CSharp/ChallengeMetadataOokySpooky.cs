using System;
using System.Collections;

public class ChallengeMetadataOokySpooky : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimHouse, bool> _003C_003E9__4_0;

		internal bool _003CMeetRequirementHauntedProperties_003Eb__4_0(SimHouse t)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetRequirementScareToDeath()
	{
		return false;
	}

	private bool MeetRequirementSummonGhosts()
	{
		return false;
	}

	private bool MeetRequirementHauntedProperties()
	{
		return false;
	}

	private bool MeetRequirementFamousMagician()
	{
		return false;
	}

	private bool MeetRequirementBornFemaleLouisiana()
	{
		return false;
	}
}
