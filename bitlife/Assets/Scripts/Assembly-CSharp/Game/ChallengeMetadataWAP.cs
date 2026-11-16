using System.Collections;

namespace Game
{
	public class ChallengeMetadataWAP : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsStripperRequirement()
		{
			return false;
		}

		private bool MeetsRapperRequirement()
		{
			return false;
		}

		private bool MeetsRecordRequirement()
		{
			return false;
		}

		private bool MeetsCatRequirement()
		{
			return false;
		}
	}
}
