using System.Collections;

namespace Game
{
	public class ChallengeMetadataWoefulWednesday : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		public bool MeetsMorticianRequirement()
		{
			return false;
		}

		public bool MeetsCelloRequirement()
		{
			return false;
		}

		public bool MeetsHauntedHomeRequirement()
		{
			return false;
		}
	}
}
