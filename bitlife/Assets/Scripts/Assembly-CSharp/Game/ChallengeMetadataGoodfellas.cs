using System.Collections;

namespace Game
{
	public class ChallengeMetadataGoodfellas : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsMafiaRequirement()
		{
			return false;
		}

		private bool MeetsUnderbossRequirement()
		{
			return false;
		}

		private ArrayList GetSpecialJobMafiaArray()
		{
			return null;
		}
	}
}
