using System.Collections;

namespace Game
{
	public class ChallengeMetadataMasterOfTheElements : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsJetSkiRequirement()
		{
			return false;
		}

		private bool MeetsAircraftRequirement()
		{
			return false;
		}

		private bool MeetsConstructionWorkerRequirement()
		{
			return false;
		}

		private bool MeetsFirefighterRequirement()
		{
			return false;
		}

		private static bool MeetsMarryRequirement()
		{
			return false;
		}

		private static bool MeetsBirthRequirements()
		{
			return false;
		}
	}
}
