using System.Collections;

namespace Game
{
	public class ChallengeMetadataFreedom : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsDeploymentRequirement()
		{
			return false;
		}
	}
}
