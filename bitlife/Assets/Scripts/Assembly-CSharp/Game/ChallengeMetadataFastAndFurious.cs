using System.Collections;

namespace Game
{
	public class ChallengeMetadataFastAndFurious : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsDodgeChargerRequirement()
		{
			return false;
		}
	}
}
