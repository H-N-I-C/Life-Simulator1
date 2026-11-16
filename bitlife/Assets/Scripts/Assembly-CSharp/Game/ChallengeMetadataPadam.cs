using System.Collections;

namespace Game
{
	public class ChallengeMetadataPadam : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		public bool FamousRequirement()
		{
			return false;
		}

		public static bool MeetsChoreographerRequirement()
		{
			return false;
		}
	}
}
