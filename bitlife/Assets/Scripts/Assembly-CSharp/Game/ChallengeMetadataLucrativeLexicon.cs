using System.Collections;

namespace Game
{
	public class ChallengeMetadataLucrativeLexicon : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsFameRequirement()
		{
			return false;
		}

		private bool MeetsBookTwoOrMoreBestSellerRequirement()
		{
			return false;
		}
	}
}
