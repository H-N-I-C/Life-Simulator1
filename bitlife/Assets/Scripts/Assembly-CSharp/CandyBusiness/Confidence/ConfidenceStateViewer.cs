using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CandyBusiness.Confidence
{
	public class ConfidenceStateViewer : MonoBehaviour
	{
		private enum Tab
		{
			Attributes = 0,
			Properties = 1,
			Dump = 2
		}

		private TextMeshProUGUI textMeshPro;

		private Button buttonCopy;

		private TextMeshProUGUI buttonCopyLabel;

		private Button buttonCopyTruncated;

		private TextMeshProUGUI buttonCopyTruncatedLabel;

		private int truncateMaxTokenChars;

		private Button buttonClose;

		private Toggle toggleAttributes;

		private Toggle toggleProperties;

		private Toggle toggleState;

		private RectTransform lineContainer;

		private ConfidenceStateLineView lineRowPrefab;

		private int maxLineChars;

		private int maxLinesPerBatch;

		private Scrollbar _fontSizeScrollBar;

		private float minFontSize;

		private float maxFontSize;

		private float defaultFontSize;

		private readonly List<ConfidenceStateLineView> _rows;

		private Coroutine _fontSizeRoutine;

		private float _targetFontSize;

		private bool _fontSizeDirty;

		private string attributesJson;

		private string propertiesJson;

		private string fullConfidenceDump;

		private Tab _activeTab;

		private const string CopyLabelCopied = "Copied!";

		private const float CopyFeedbackSeconds = 3f;

		private Coroutine _copyFeedbackRoutine;

		private string _buttonCopyOriginalText;

		private string _buttonCopyTruncatedOriginalText;
	}
}
