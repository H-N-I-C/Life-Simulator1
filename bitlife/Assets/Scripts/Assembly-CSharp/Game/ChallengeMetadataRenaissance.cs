using System.Collections;

namespace Game
{
	public class ChallengeMetadataRenaissance : ChallengeMetadata
	{
		public override ArrayList GetRequirementsInProgressArray()
		{
			return null;
		}

		private bool MeetRequirementBornMaleInItaly()
		{
			return false;
		}

		private bool MeetPhysicsDegreeRequirement()
		{
			return false;
		}

		private bool MeetGraphicDesignDegreeRequirement()
		{
			return false;
		}

		private bool MeetPainterRequirement()
		{
			return false;
		}

		private bool MeetLongWalkAfter18Requirement()
		{
			return false;
		}
	}
}
