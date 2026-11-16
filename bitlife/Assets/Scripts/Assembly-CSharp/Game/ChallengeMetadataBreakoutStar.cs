using System.Collections;

namespace Game
{
	public class ChallengeMetadataBreakoutStar : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MagazineRequirement()
		{
			return false;
		}

		private bool BurgleRequirement()
		{
			return false;
		}

		private bool FamousRequirement()
		{
			return false;
		}

		private bool CheckBreakoutPrisonByType(int breakoutType)
		{
			return false;
		}
	}
}
