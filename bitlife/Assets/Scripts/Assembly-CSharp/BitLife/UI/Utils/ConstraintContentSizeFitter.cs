using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Utils
{
	public class ConstraintContentSizeFitter : MonoBehaviour
	{
		private bool controlHeight;

		private bool controlWidth;

		private bool stackedVertically;

		private RectTransform[] drivers;

		private RectTransform[] maxHeightPerDriver;

		private RectTransform rect;

		private float sideMargin;

		private float topAndBottomMargin;

		private float adBannerSpace;

		private bool accountForAdBanner;

		private VerticalLayoutGroup verticalLayoutGroup;

		private float minWidth;

		private float minHeight;

		private RectTransform minHeightDriver;

		private RectTransform maxHeightDriver;

		private List<RectTransform> maxHeightRecantedDrivers;

		private readonly List<RectTransform> runtimeMaxHeightRecantedDrivers;

		private VerticalLayoutGroup maxHeightRecantVerticalLayoutGroup;

		private float maxHeightRecantBottomOffset;

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

		public void AddMaxHeightRecantedDriver(RectTransform driver)
		{
		}

		public RectTransform[] GetDrivers()
		{
			return null;
		}

		public void SetDriver(RectTransform[] drivers)
		{
		}

		public void AddDriver(RectTransform driver)
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
