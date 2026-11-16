using System.Collections;

namespace Game
{
	public class ChallengeMetadataKingOfTheCourt : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetRequirementMaleBornInJapan()
		{
			return false;
		}

		private bool MeetVolleyballCaptainRequirement()
		{
			return false;
		}

		private bool MeetBefriendEnemyRequirement()
		{
			return false;
		}

		private bool MeetGym10TimesRequirement()
		{
			return false;
		}

		private bool MeetBrazilVacationRequirement()
		{
			return false;
		}
	}
}
