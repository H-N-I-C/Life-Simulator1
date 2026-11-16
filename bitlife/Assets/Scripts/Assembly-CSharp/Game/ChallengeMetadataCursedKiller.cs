using System.Collections;

namespace Game
{
	public class ChallengeMetadataCursedKiller : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsExorciseRequirement()
		{
			return false;
		}

		private bool MeetsMartialArtsRequirement()
		{
			return false;
		}

		private bool DidBecomeExorcist()
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
