using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CandyBusiness.Confidence
{
	public class ConfidenceStateLineView : MonoBehaviour
	{
		private TMP_Text label;

		private Toggle foldout;

		private TMP_Text foldoutLabel;

		private TMP_Text overflow;

		private GameObject overflowContent;

		private float _currentFontSize;

		public void InitAsPlainText(string block)
		{
		}

		public void InitAsTruncatedText(string line, int visibleChars)
		{
		}

		private void SetExpanded(bool expanded)
		{
		}

		public void SetFontSize(float size)
		{
		}

		private GameObject OverflowContentObject()
		{
			return null;
		}
	}
}
