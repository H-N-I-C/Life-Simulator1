using System.Collections;

namespace Game
{
	public class ChallengeMetadataTheButcher : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsButcherInRomaniaRequirement()
		{
			return false;
		}

		private bool MeetsEmigrateToUSAfterEscapingPrisonRequirement()
		{
			return false;
		}
	}
}
