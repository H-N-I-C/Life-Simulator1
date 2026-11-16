using UnityEngine;

namespace BitLife.UI.Utils
{
	public class ConstraintScaleStartSeqUI : MonoBehaviour
	{
		public static ConstraintScaleStartSeqUI Inst;

		public readonly float BASE_WIDTH;

		public readonly float BASE_HEIGHT;

		private const float TABLET_THRESHOLD = 1.47f;

		private RectTransform rect;

		private RectTransform safeZone;

		private float scale;

		private float safeWidth;

		private float safeHeight;

		private bool isOnTablet;

		private void Awake()
		{
		}

		public float GetWidth()
		{
			return 0f;
		}

		public float GetScale()
		{
			return 0f;
		}

		public float GetHeight()
		{
			return 0f;
		}

		public float GetSafeHeight()
		{
			return 0f;
		}

		public float GetSafeHeightDifference()
		{
			return 0f;
		}

		public bool IsOnTablet()
		{
			return false;
		}

		private void UpdateConstraint()
		{
		}
	}
}
