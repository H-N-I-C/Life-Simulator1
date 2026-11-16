using System;
using System.Collections;

public class ChallengeMetadataNoReservations : ChallengeMetadata
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimEmigration, string> _003C_003E9__2_0;

		internal string _003CTravelCountriesVisitedRequirement_003Eb__2_0(SimEmigration t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public string heroCountry;

		internal bool _003CTravelCountriesVisitedRequirement_003Eb__1(string t)
		{
			return false;
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool TravelCountriesVisitedRequirement()
	{
		return false;
	}

	public bool MeetsBestSellerRequirement()
	{
		return false;
	}

	public bool MeetsFamousChefRequirement()
	{
		return false;
	}
}
