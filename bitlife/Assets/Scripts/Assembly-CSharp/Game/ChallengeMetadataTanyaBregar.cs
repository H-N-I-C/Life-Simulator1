using System.Collections;

namespace Game
{
	public class ChallengeMetadataTanyaBregar : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetsRabbitRequirement()
		{
			return false;
		}

		public bool MeetsFollowerRequirement()
		{
			return false;
		}
	}
}
