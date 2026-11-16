using System;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyTableOptions : IguiOptions
	{
		public float desiredWidth;

		public Dictionary<string, string> tableEntryPairs;

		public int verticalInset;

		public int horizontalInset;

		public FontsEnum fontName;

		public float fontSize;

		public Color fontColor;

		public Color primaryBgColor;

		public Color secondaryBgColor;

		public bool recognizeNegativeCurrency;

		public bool useValueColorForKeyColor;

		public Action<int> delegateCallbackMethodForInt;

		public Action delegateCallbackMethod;

		public Action<List<CandyText>> delegateCallbackMethodForCustomEntries;

		public CandyTableOptions(float desiredWidth, Dictionary<string, string> tableEntryPairs, int verticalInset, int horizontalInset, FontsEnum fontName, float fontSize, Color fontColor, Color primaryBgColor, Color secondaryBgColor, bool recognizeNegativeCurrency, bool useValueColorForKeyColor, Action<int> delegateCallbackMethodForInt = null, Action delegateCallbackMethod = null)
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
