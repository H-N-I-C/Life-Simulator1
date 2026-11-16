using System.Collections.Generic;
using Game.Seasons;

namespace Tracking
{
	public class BitLife_TaskComplete : GameTelemetryEvent
	{
		private string TaskCompletedRightNow;

		private string IsAbleToDoubleWithRV;

		private string IsAbleToDoubleWithBitpass;

		private string EarnedBitsBase;

		private string EarnedBitsRV;

		private string EarnedBitsBitpassBonus;

		private Dictionary<ObjectiveType, (string, string)> TaskStatuses;

		private Dictionary<ObjectiveType, string> TaskStatusNames;

		public BitLife_TaskComplete(List<string> objectiveIds, int earnedBase, int earnedVideo, int earnedBitPass, bool videoAvailable)
		{
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
