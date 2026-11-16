using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public sealed class UIDailyMarketplaceHeader : MonoBehaviour
	{
		private TextMeshProUGUI sectionLabel;

		private TextMeshProUGUI balanceValue;

		private Image balanceIcon;

		private TMP_FontAsset normalFont;

		private Material normalMaterial;

		private float normalFontSize;

		private float normalSpacing;

		private float normalFontSizeMin;

		private float normalFontSizeMax;

		private Color normalColor;

		private bool normalAutoSizing;

		private TextWrappingModes normalWrapping;

		private void Start()
		{
		}

		public void Bind(string text, long? balance = null)
		{
		}
	}
}
