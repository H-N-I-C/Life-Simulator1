using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIAdBannerBacker : MonoBehaviour
	{
		private const float ANIM_DUR = 0.2f;

		private const float WIDTH = 1242f;

		private RectTransform rect;

		private LayoutElement spacer;

		private CandyButton dismissButton;

		private CandyImage topLine;

		private AccessibleUIGroupRoot uapGroup;

		private bool isShowingAds;

		private bool AdsAreOff;

		private float height;

		public AccessibleUIGroupRoot UapGroup => null;

		private void Start()
		{
		}

		public void SetTheme()
		{
		}

		private float UpdateHeight()
		{
			return 0f;
		}

		private void DoPurchaseIAP()
		{
		}

		public void TurnOff()
		{
		}

		public void TurnOn()
		{
		}

		public float GetHeight()
		{
			return 0f;
		}

		public void Hide()
		{
		}

		public void Show()
		{
		}

		private void Remove()
		{
		}
	}
}
