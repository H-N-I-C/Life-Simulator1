using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyBarGraphOptions : IguiOptions
	{
		public Vector2 size;

		public float value;

		public Color backgroundColor;

		public Color lowValueBarColor;

		public Color mediumValueBarColor;

		public Color highValueBarColor;

		public float lowValueMax;

		public float mediumValueMax;

		public int bubbleCount;

		public BarGraphSourceType sourceType;

		public CandyBarGraphOptions()
		{
		}

		public CandyBarGraphOptions(Vector2 size, float value, Color backgroundColor, Color lowValueBarColor, Color mediumValueBarColor, Color highValueBarColor, float lowValueMax, float mediumValueMax, int bubbleCount = 1)
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
