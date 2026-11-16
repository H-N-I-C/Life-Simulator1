using System.Collections;

namespace Game
{
	public class ChallengeMetadataSpareChange : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool EarnRequirement()
		{
			return false;
		}

		private float AmountEarningsFromPanhandling(SimOccupationSpecial special)
		{
			return 0f;
		}

		private bool FameActivityRequirement()
		{
			return false;
		}

		private bool EmigrateUSRequirement()
		{
			return false;
		}

		private bool MarryCommonerRequirement()
		{
			return false;
		}

		private bool BornPrinceRequirement()
		{
			return false;
		}
	}
}
