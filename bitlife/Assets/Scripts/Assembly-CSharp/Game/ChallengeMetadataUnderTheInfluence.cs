using System.Collections;

namespace Game
{
	public class ChallengeMetadataUnderTheInfluence : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetRequirementBornInGermany()
		{
			return false;
		}

		private bool MeetVerifiedSocialMediaRequirement()
		{
			return false;
		}

		private bool MeetAlcoholAddictionRequirement()
		{
			return false;
		}

		private bool MeetGamblingAddictionRequirement()
		{
			return false;
		}

		private bool MeetTrollRequirement()
		{
			return false;
		}
	}
}
