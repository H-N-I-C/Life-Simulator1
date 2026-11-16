using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI
{
	public sealed class InfoButtonCallout : MonoBehaviour
	{
		private RectTransform reveal;

		private RectTransform capsule;

		private TextMeshProUGUI label;

		private Image ring;

		private float horizontalPadding;

		private float verticalPadding;

		private float minimumHeight;

		private float safeAreaPadding;

		private float revealSeconds;

		private bool presented;

		private bool acknowledged;

		private bool labelSuppressed;

		private bool animating;

		private bool enabledOnce;

		private float progress;

		private double revealStartedAt;

		private float fullWidth;

		private Rect lastSafeArea;

		private Vector3 lastPosition;

		private Vector3 lastScale;

		private Canvas parentCanvas;

		private bool HasReferences => false;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void ShowOnce(string text)
		{
		}

		public void SetText(string text)
		{
		}

		public void Dismiss()
		{
		}

		public void SetLabelSuppressed(bool suppressed)
		{
		}

		private void LateUpdate()
		{
		}

		private void RefreshLayout()
		{
		}

		private float AvailableWidth()
		{
			return 0f;
		}

		private void ApplyReveal()
		{
		}

		private void SetVisible(bool visible)
		{
		}
	}
}
