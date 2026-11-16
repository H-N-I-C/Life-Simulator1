using System.Collections;

namespace Game
{
	public class ChallengeMetadataFullGlam : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsMakeupArtistRequirement()
		{
			return false;
		}

		private bool MeetsFamousSocialMediaWhilMakeupArtistRequirement()
		{
			return false;
		}

		private bool MeetsNoseJobRequirement()
		{
			return false;
		}
	}
}
