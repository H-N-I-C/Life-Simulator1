using System.Collections;

namespace Game
{
	public class ChallengeMetadataNomad : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool BeBoreInTheUnitedStates(SimHero hero)
		{
			return false;
		}

		private bool EmigrateToGermany(Life heroLife)
		{
			return false;
		}

		private bool EmigrateToSpain(Life heroLife)
		{
			return false;
		}

		private bool EmigrateToFrance(Life heroLife)
		{
			return false;
		}

		private bool EmigrateToBrazil(Life heroLife)
		{
			return false;
		}

		private bool LiveOrHaveLiveIn(Life heroLife, string country)
		{
			return false;
		}
	}
}
