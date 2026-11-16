using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Utils
{
	public class ConstraintContentSizeFitterByLayoutElement : MonoBehaviour
	{
		private bool controlHeight;

		private bool controlWidth;

		private bool stackedVertically;

		private LayoutElement[] drivers;

		private RectTransform rect;

		private float sideMargin;

		private float topAndBottomMargin;

		private VerticalLayoutGroup verticalLayoutGroup;

		private float minWidth;

		private float minHeight;

		private RectTransform minHeightDriver;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void UpdateConstraint(bool debug = false)
		{
		}

		public void SetMinHeightDriver(RectTransform driver)
		{
		}

		public void SetDriver(LayoutElement[] drivers)
		{
		}

		public void AddDriver(LayoutElement driver)
		{
		}

		public void SetControlHeight(bool isControlingHeight)
		{
		}

		public void SetControlWidth(bool isControlingWidth)
		{
		}
	}
}
