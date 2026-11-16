using System.Collections;

namespace Game
{
	public class ChallengeMetadataAltruisticAviator : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		public static bool MeetsPilotRequirement()
		{
			return false;
		}
	}
}
