using System.Collections;

namespace Game
{
	public class ChallengeMetadataDeliverance : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsBornMaleInLouisianaRequirement()
		{
			return false;
		}

		private bool MeetsBanjoRequirement()
		{
			return false;
		}

		private bool MeetsUpgradeRequirement()
		{
			return false;
		}

		private bool MeetsTrailersRequirement()
		{
			return false;
		}
	}
}
