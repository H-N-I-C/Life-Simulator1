using BitLife.UI.Containers;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIMultiPurposeButtonManager : MonoBehaviour
	{
		private RectTransform rect;

		private UIMultiPurposeButton buttonPrefab;

		private bool DisableMultiPurposeButtonVisibilityUpdating__BackingField;

		private UIMultiPurposeButton communityButton;

		private UIMultiPurposeButton heirloomButton;

		private UIMultiPurposeButton prayButton;

		private UIMultiPurposeButton customPersonButton;

		private UIMultiPurposeButton webshopSalesButton;

		public bool DisableMultiPurposeButtonVisibilityUpdating
		{
			get
			{
				return DisableMultiPurposeButtonVisibilityUpdating__BackingField;
			}
			set
			{
				DisableMultiPurposeButtonVisibilityUpdating__BackingField = value;
			}
		}

		public void Init()
		{
		}

		public void SetupButtons()
		{
		}

		public void UpdateMultiPurposeButtonVisibility()
		{
		}

		private void ShowMultiPurposeButton()
		{
		}

		public void HideMultiPurposeButton()
		{
		}

		private static void HideIfShowing(UIMultiPurposeButton button, ref float delay)
		{
		}
	}
}
