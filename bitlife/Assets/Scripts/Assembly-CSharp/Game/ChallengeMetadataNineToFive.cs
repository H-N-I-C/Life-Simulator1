using System.Collections;

namespace Game
{
	public class ChallengeMetadataNineToFive : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsBornFemaleInCaliforniaRequirement()
		{
			return false;
		}

		private bool MeetsReceptionistRequirement()
		{
			return false;
		}

		private bool MeetsBreastEnhancementSurgeryRequirement()
		{
			return false;
		}
	}
}
