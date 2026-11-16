using System.Collections;

namespace Game
{
	public class ChallengeMetadataRocketMan : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsPianoRequirement()
		{
			return false;
		}

		private bool MeetsSoloArtistRequirement()
		{
			return false;
		}

		private bool MeetsRecordRequirement()
		{
			return false;
		}

		private bool MeetsMarriageRequirement()
		{
			return false;
		}
	}
}
