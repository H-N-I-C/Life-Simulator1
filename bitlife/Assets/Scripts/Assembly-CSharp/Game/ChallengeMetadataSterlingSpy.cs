using System.Collections;

namespace Game
{
	public class ChallengeMetadataSterlingSpy : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsCEORequirement()
		{
			return false;
		}

		private bool MeetsSpyAgencyRequirement()
		{
			return false;
		}

		private bool MeetsStealSpyAgencyRequirement()
		{
			return false;
		}

		private static bool MeetsMurderRequirement()
		{
			return false;
		}

		private static bool MeetsBirthRequirements()
		{
			return false;
		}
	}
}
