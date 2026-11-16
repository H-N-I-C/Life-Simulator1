using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandySliderOptions : IguiOptions
	{
		public delegate string ReturnStringFromFloat(float value);

		public float width;

		public CandyImageOptions thumbHandleOptions;

		public CandyTextOptions titleCandyTextOptions;

		public CandyTextOptions valueCandyTextOptions;

		public float value;

		public float minValue;

		public float maxValue;

		public float lineThickness;

		public Color lineColor;

		public bool hideValue;

		public ReturnStringFromFloat delegateMethod_StringFromValue;

		public CandySliderOptions(float width, CandyImageOptions thumbHandleOptions, CandyTextOptions titleCandyTextOptions, CandyTextOptions valueCandyTextOptions, float value, float minValue, float maxValue, float lineThickness, Color lineColor, ReturnStringFromFloat delegateMethod_StringFromValue = null, bool hideValue = false)
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
