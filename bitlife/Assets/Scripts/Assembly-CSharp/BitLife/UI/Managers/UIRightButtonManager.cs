using System.Collections.Generic;
using BitLife.UI.Containers;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIRightButtonManager : MonoBehaviour
	{
		private static Dictionary<RIGHT_SIDE_BUTTON_TYPE, string> ButtonPrefab;

		private static UIRightButtonManager instance;

		private UIRightButton marketplaceButton;

		private RIGHT_SIDE_BUTTON_TYPE currentMarketplaceType;

		private RIGHT_SIDE_BUTTON_TYPE currentShownType;

		private bool _hudActive;

		private static void InitBeforeSceneLoad()
		{
		}

		private static void OnConfidenceHomeScreen()
		{
		}

		public void Init()
		{
		}

		private void UpdateMarketplaceType(RIGHT_SIDE_BUTTON_TYPE nextMarketplaceType)
		{
		}

		private UIRightButton LoadMarketplaceButton(RIGHT_SIDE_BUTTON_TYPE buttonType)
		{
			return null;
		}

		public void SetupButtons()
		{
		}

		private void SetupMarketplaceButton()
		{
		}

		public void UpdateRightButtonVisibility(float delay = 0.35f, float animTime = 0.35f)
		{
		}

		private void ShowRightButton(RIGHT_SIDE_BUTTON_TYPE nextShownType, float delay = 0f, float animTime = 0.35f)
		{
		}

		public void HideRightButton()
		{
		}

		public void SetMarketplaceButtonHudActive(bool hudActive)
		{
		}

		private void HideMarketplaceButton()
		{
		}
	}
}
