using System.Collections;

namespace Game
{
	public class ChallengeMetadataManicMother : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		public bool MeetsBabyDaddyRequirement()
		{
			return false;
		}

		private bool MeetsChildInsultRequirement()
		{
			return false;
		}

		private bool MeetsChildAbandonRequirement()
		{
			return false;
		}

		private bool MeetsMarriageRequirement()
		{
			return false;
		}

		private bool MeetsStepchildInsultRequirement()
		{
			return false;
		}
	}
}
