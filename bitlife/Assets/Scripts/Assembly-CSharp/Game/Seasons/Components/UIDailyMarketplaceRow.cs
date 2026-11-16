using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public sealed class UIDailyMarketplaceRow : MonoBehaviour
	{
		private const float FullPriceFontSize = 22.4f;

		private const float EffectivePriceFontSize = 33.94f;

		private TextMeshProUGUI nameLabel;

		private TextMeshProUGUI descriptionLabel;

		private TextMeshProUGUI priceLabel;

		private TextMeshProUGUI discountLabel;

		private Image productIcon;

		private GameObject discount;

		private GameObject owned;

		private GameObject price;

		private DailyMarketplaceOfferModel offer;

		private string productDescription;

		private bool isOwnedRow;

		private bool productMetadataPending;

		private float nextTick;

		private TextMeshProUGUI durationLabel;

		private TextMeshProUGUI regularPriceLabel;

		private Image regularPriceStrike;

		private void Start()
		{
		}

		public void Bind(DailyMarketplaceOfferModel data, bool ownedRow = false)
		{
		}

		private void ApplyProductMetadata(ListItem product)
		{
		}

		private void ApplyReferenceLayout()
		{
		}

		private static void FitTranslatedLine(TextMeshProUGUI label, float maximum, float minimum)
		{
		}

		private static void Style(TextMeshProUGUI label, TMP_FontAsset font, float size, Color color, TextAlignmentOptions alignment = TextAlignmentOptions.MidlineLeft)
		{
		}

		private static void Place(RectTransform rect, float x, float y, float width, float height)
		{
		}

		private static void PlaceRight(RectTransform rect, float inset, float y, float width, float height)
		{
		}

		private static void Stretch(RectTransform rect, float left, float right, float y, float height)
		{
		}

		private void PlacePriceStrike(float x, float y, float width)
		{
		}

		private void LayoutPrices(bool hasSale)
		{
		}

		private static float MeasurePrice(TextMeshProUGUI label, float fontSize)
		{
			return 0f;
		}

		private void Update()
		{
		}

		private void UpdateDescription()
		{
		}
	}
}
