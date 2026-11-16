using System.Collections;

namespace Game
{
	public class ChallengeMetadataBabyBoomer : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetRequirementFemaleBornInNewYork()
		{
			return false;
		}

		private bool MeetNurseRequirement()
		{
			return false;
		}

		private bool Meet5BabiesInNewYorkRequirement()
		{
			return false;
		}

		private bool Meet5BabiesInArgentinaRequirement()
		{
			return false;
		}

		private bool Meet5BabiesInItalyRequirement()
		{
			return false;
		}
	}
}
