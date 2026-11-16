using System.Collections;

namespace Game
{
	public class ChallengeMetadataAlphabetMafia : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsCeoShakedownRequirement()
		{
			return false;
		}

		private bool MeetsComeOutRequirement()
		{
			return false;
		}

		private bool MeetsMafiaRequirement()
		{
			return false;
		}

		private bool MeetsMafiaLeaderRequirement()
		{
			return false;
		}

		private ArrayList GetSpecialJobMafiaArray()
		{
			return null;
		}

		private static bool MeetsBirthRequirements()
		{
			return false;
		}
	}
}
