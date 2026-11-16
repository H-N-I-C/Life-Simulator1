using System.Collections;

namespace Game
{
	public class ChallengeMetadataFullMetalSoldier : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool DischargeRequirement()
		{
			return false;
		}

		private bool DeploymentRequirement()
		{
			return false;
		}

		private bool MurderCommanderRequirement()
		{
			return false;
		}

		private bool HookupEnlisteeRequirement()
		{
			return false;
		}

		private bool EnlistRequirement()
		{
			return false;
		}
	}
}
