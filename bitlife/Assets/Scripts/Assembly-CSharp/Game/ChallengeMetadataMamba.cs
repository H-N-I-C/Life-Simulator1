using System.Collections;

namespace Game
{
	public class ChallengeMetadataMamba : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsChampionshipRequirement()
		{
			return false;
		}

		private bool MeetsMVPAwardRequirement()
		{
			return false;
		}
	}
}
