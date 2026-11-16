using System.Text.RegularExpressions;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using TMPro;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyText : MonoBehaviour, IguiObject
	{
		private static string PATTERN;

		private RectTransform rect;

		private TextMeshProUGUI tmpro;

		private float width;

		private CandyImage emoji;

		public CandyTextOptions options;

		private TMProUGUIHyperlinks hyperlink;

		private string _translatedText;

		private string _originalText;

		public RectTransform Rect => null;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public bool IsOnScreenCompletely()
		{
			return false;
		}

		public bool IsOffScreenCompletely()
		{
			return false;
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void SetupUAP(bool isUAP, string labelAddition)
		{
		}

		public void SetHorizontalPos(float posX)
		{
		}

		public void SetVerticalPos(float posY)
		{
		}

		public void SetPos(float posX, float posY)
		{
		}

		public float GetPosX()
		{
			return 0f;
		}

		public float GetPosY()
		{
			return 0f;
		}

		public RectTransform GetRect()
		{
			return null;
		}

		public TextMeshProUGUI GetTMPro()
		{
			return null;
		}

		public float GetPosOfSingleCandyTextRightEdge()
		{
			return 0f;
		}

		public float GetRectWidth()
		{
			return 0f;
		}

		public float GetRectHeight()
		{
			return 0f;
		}

		public float GetTMProPreferredWidth()
		{
			return 0f;
		}

		public float GetTMProPrefMinWidth()
		{
			return 0f;
		}

		public float GetTMProPreferredHeight()
		{
			return 0f;
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetNonHyperLinkText(string text)
		{
		}

		public void SetText(string text)
		{
		}

		public string LinkTagReplacement(Match match)
		{
			return null;
		}

		public string LinkTagReplacementCatLife(Match match)
		{
			return null;
		}

		public string LinkTagReplacementRankLife(Match match)
		{
			return null;
		}

		private bool IsThisElementNestedInADarkly(Transform parent)
		{
			return false;
		}

		public string GetText()
		{
			return null;
		}

		public string GetOriginalText()
		{
			return null;
		}

		public string GetUntranslatedText()
		{
			return null;
		}

		public void SetOriginalText(string text)
		{
		}

		public void SetTranslatedLabel(string text)
		{
		}

		public Color GetFontColor()
		{
			return default;
		}

		public int GetFontNameIndex()
		{
			return 0;
		}

		public void AddToText(string text)
		{
		}

		public void SetFontName(FontsEnum fontName)
		{
		}

		public void SetFontSize(float fontSize)
		{
		}

		public void SetFontColor(Color color)
		{
		}

		public void SetFontColorAlpha(float alpha)
		{
		}

		public void SetFontColorDirect(Color color)
		{
		}

		public void SetFontColorVerticalGradient(Color topColor, Color bottomColor)
		{
		}

		public void SetFontColorFourCornerGradient(Color topLeftColor, Color topRightColor, Color bottomLeftColor, Color bottomRightColor)
		{
		}

		public void SetIsUnderlined(bool isUnderlined)
		{
		}

		public void SetAlignment(TextAlignmentEnum alignment)
		{
		}

		public void ForceUpdate()
		{
		}

		public void SetIsSingleLine(bool isSingleLine)
		{
		}

		public void SetWidth(float width)
		{
		}

		public void SetEmojiImage(CandyImageOptions candyImageOptions = null)
		{
		}

		public float GetEmojiWidth()
		{
			return 0f;
		}

		public void SetEmojiSpacer(float space, bool useTextWidthAsTotalSpaceAvailable = false, bool setEmojiSizeToMatchText = true, bool adjustToCenter = true, bool useLayoutForSlider = false)
		{
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}
	}
}
