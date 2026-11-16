using System.Collections;

namespace Game
{
	public class ChallengeMetadataDemonSlayer : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsSisterRequirement()
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
	}
}
