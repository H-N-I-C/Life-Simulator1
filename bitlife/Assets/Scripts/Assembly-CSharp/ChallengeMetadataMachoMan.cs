using System;
using System.Collections;

public class ChallengeMetadataMachoMan : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOccupationEmployee, bool> _003C_003E9__2_0;

		public static Func<SimChild, bool> _003C_003E9__7_0;

		internal bool _003CHasHadJob_003Eb__2_0(SimOccupationEmployee e)
		{
			return false;
		}

		internal bool _003CMeetRequirement5_003Eb__7_0(SimChild c)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool HasHadJob(string targetCareerName)
	{
		return false;
	}

	private bool MeetRequirement1()
	{
		return false;
	}

	private bool MeetRequirement2()
	{
		return false;
	}

	private bool MeetRequirement3()
	{
		return false;
	}

	private bool MeetRequirement4()
	{
		return false;
	}

	private bool MeetRequirement5()
	{
		return false;
	}
}
