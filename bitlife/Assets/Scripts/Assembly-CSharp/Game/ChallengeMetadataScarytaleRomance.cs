using System.Collections;

namespace Game
{
	public class ChallengeMetadataScarytaleRomance : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsBornFemaleInColoradoRequirement()
		{
			return false;
		}

		private bool MeetsPsychiatristtRequirement()
		{
			return false;
		}
	}
}
