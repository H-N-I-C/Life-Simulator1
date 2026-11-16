using System.Collections;

namespace Game
{
	public class ChallengeMetadataAntiHero : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		public static bool MeetsFirefighterRequirement()
		{
			return false;
		}
	}
}
