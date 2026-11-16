using System.Collections;

namespace Game
{
	public class ChallengeMetadataPride : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsAdoptionRequirement()
		{
			return false;
		}
	}
}
