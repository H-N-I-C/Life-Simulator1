using System.Collections;

namespace Game
{
	public class ChallengeMetadataPatientZero : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsBornMaleInFloridaRequirement()
		{
			return false;
		}

		private bool MeetsRoadkillRequirement()
		{
			return false;
		}

		private bool MeetsSTDRequirement()
		{
			return false;
		}

		private bool MeetsMarriageRequirement()
		{
			return false;
		}

		private bool MeetsDiseaseRequirement()
		{
			return false;
		}
	}
}
