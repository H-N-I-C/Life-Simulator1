using System.Collections;

namespace Game
{
	public class ChallengeMetadataRedQueen : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsQueenRequirement()
		{
			return false;
		}

		private bool MeetsMarriageRequirement()
		{
			return false;
		}

		private bool MeetsHouseRequirement()
		{
			return false;
		}
	}
}
