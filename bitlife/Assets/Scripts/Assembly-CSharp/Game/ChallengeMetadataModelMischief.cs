using System.Collections;

namespace Game
{
	public class ChallengeMetadataModelMischief : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private static bool MeetsFamousModelRequirement()
		{
			return false;
		}

		private static bool MeetsSabotagedModelRequirement()
		{
			return false;
		}

		private static bool MeetsMurderedModelRequirement()
		{
			return false;
		}

		private static bool MeetsFashionWeeksRequirement()
		{
			return false;
		}

		private static bool MeetsBirthRequirements()
		{
			return false;
		}
	}
}
