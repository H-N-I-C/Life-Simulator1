using System;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyTextOptions : IguiOptions
	{
		private const float FONT_SIZE = 20f;

		private static readonly Color FONT_COLOR;

		private const bool IS_UNDERLINED = false;

		private const bool IS_SINGLE_LINE = false;

		private const float WIDTH = 100f;

		private const float EMOJI_SPACING = 100f;

		private const FontsEnum FONT_NAME = FontsEnum.AvenirNextRegular;

		public string text;

		public string originalText;

		public float fontSize;

		private Color fontColor__BackingField;

		private Color fontColorBottom__BackingField;

		public bool isUnderlined;

		public bool isSingleLine;

		public FontsEnum fontName;

		public TextAlignmentEnum alignment;

		public float width;

		public float emojiSpacing;

		public CandyImageOptions emojiOptions;

		public bool isUAP;

		public string labelAddition;

		public string customSoundFileName;

		public bool useLayoutForSlider;

		public Dictionary<Type, ScenarioComponentEvent> uiEvents;

		public Color fontColor
		{
			get
			{
				return fontColor__BackingField;
			}
			private set
			{
				fontColor__BackingField = value;
			}
		}

		public Color fontColorBottom
		{
			get
			{
				return fontColorBottom__BackingField;
			}
			private set
			{
				fontColorBottom__BackingField = value;
			}
		}

		public CandyTextOptions(string text, Color fontColor, float fontSize = 20f, float width = 100f, float emojiSpacing = 100f, CandyImageOptions emojiOptions = null, bool isUnderlined = false, bool isSingleLine = false, FontsEnum fontName = FontsEnum.AvenirNextRegular, TextAlignmentEnum alignment = TextAlignmentEnum.center, bool isUAP = false, string labelAddition = "", string customSoundFileName = null, bool useLayoutForSlider = false, string originalText = "")
		{
		}

		public CandyTextOptions(string text, Color fontColor, Color fontColorBottom, float fontSize = 20f, float width = 100f, float emojiSpacing = 100f, CandyImageOptions emojiOptions = null, bool isUnderlined = false, bool isSingleLine = false, FontsEnum fontName = FontsEnum.AvenirNextRegular, TextAlignmentEnum alignment = TextAlignmentEnum.center, bool isUAP = false, string labelAddition = "", string customSoundFileName = null, bool useLayoutForSlider = false)
		{
		}

		public void SetFontColor(Color color)
		{
		}

		public void SetFontColor(Color color, Color bottomColor)
		{
		}

		public Vector2 GetSize()
		{
			return default;
		}

		public bool HasTexture()
		{
			return false;
		}
	}
}
