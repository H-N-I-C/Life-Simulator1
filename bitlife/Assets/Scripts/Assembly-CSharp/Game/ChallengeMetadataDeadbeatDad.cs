using System.Collections;

namespace Game
{
	public class ChallengeMetadataDeadbeatDad : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsFatherRequirement()
		{
			return false;
		}

		private bool MeetsChildAbandonRequirement()
		{
			return false;
		}
	}
}
