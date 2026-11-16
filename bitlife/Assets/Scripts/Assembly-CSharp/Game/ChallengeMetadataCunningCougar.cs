using System.Collections;

namespace Game
{
	public class ChallengeMetadataCunningCougar : ChallengeMetadata
	{
		private struct AgeAndLoverParameters
		{
			public int HeroAge;

			public SimLover CurrentLover;

			public ArrayList PreviousLovers;
		}

		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool BeBornAFemaleInCanada(SimHero hero)
		{
			return false;
		}

		private bool BecomeAForensicScientist(SimHero hero, Life heroLife)
		{
			return false;
		}

		private bool HookUpWith5OrMorePeople10OrMoreYearsYoungerThanYou(Apollo16ChallengeTracker challengeTracker)
		{
			return false;
		}

		private bool MarrySomeone10OrMoreYearsYoungerThanYou(AgeAndLoverParameters parameters)
		{
			return false;
		}

		private bool IsForensicScientist(SimOccupation job)
		{
			return false;
		}

		private bool TenYearsOlderAndStatus(int heroAge, SimLover lover, LOVER_RELATIONSHIP_STATUS loverStatus)
		{
			return false;
		}

		private bool HaveTwinsAfterAge35(Apollo16ChallengeTracker challengeTracker)
		{
			return false;
		}

		public static void CheckForHookup(SimLover lover)
		{
		}
	}
}
