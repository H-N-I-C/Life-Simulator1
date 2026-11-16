using System.Collections;

public class ChallengeMetadataFoolsRush : ChallengeMetadata
{
	private class DivorceEvent
	{
		private readonly string SpouseName__BackingField;

		private readonly int MarriageAge__BackingField;

		private readonly int DivorceAge__BackingField;

		public string SpouseName => SpouseName__BackingField;

		public int MarriageAge => MarriageAge__BackingField;

		public int DivorceAge => DivorceAge__BackingField;

		public DivorceEvent(string spouseName, int marriageAge, int divorceAge)
		{
		}
	}

	public override ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}

	private bool MeetsBornInParisRequirement()
	{
		return false;
	}

	private bool MeetsFiveDivorcesInFiveYearsRequirement()
	{
		return false;
	}
}
