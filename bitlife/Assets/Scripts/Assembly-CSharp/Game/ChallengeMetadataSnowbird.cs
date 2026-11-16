using System.Collections;

namespace Game
{
	public class ChallengeMetadataSnowbird : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsBornInIcelandeRequirement()
		{
			return false;
		}

		private bool MeetsEmigrationRequirement()
		{
			return false;
		}

		private bool MeetsUpgradeRequirement()
		{
			return false;
		}
	}
}
