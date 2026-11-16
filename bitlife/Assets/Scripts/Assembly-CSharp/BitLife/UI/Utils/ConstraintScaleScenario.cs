using UnityEngine;

namespace BitLife.UI.Utils
{
	public class ConstraintScaleScenario : MonoBehaviour
	{
		private const float VERTICAL_SPACE = 200f;

		private RectTransform safeZone;

		private RectTransform rect;

		private RectTransform logoBannerRect;

		private float MaxScale;

		private bool accountForAdBanner;

		private float adBannerSpace;

		private float scale;

		private float safeWidth;

		private float safeHeight;

		private ConstraintScaleBgUI constraintScaleBgUI;

		public void ResetScaleToOne()
		{
		}

		public float GetScale()
		{
			return 0f;
		}

		public void UpdateConstraint(float scenarioWidth = 0f, float scenarioHeight = 0f)
		{
		}

		public Vector2 GetPosMindingAdBannerBacker()
		{
			return default;
		}

		public bool IsAccountingForAdBannerBacker()
		{
			return false;
		}
	}
}
