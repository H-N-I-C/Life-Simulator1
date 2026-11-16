using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIDevModeDisplayManager : MonoBehaviour
	{
		private const int AVG_TOTAL = 10;

		private CandyText FPSCount;

		private CandyText DPICount;

		private CandyText SizeCount;

		private int[] recentFrameRates;

		private int currentRecentFrameIndex;

		private void Start()
		{
		}

		private void Update()
		{
		}
	}
}
