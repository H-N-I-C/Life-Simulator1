using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.SupportObjects
{
	public class UIStatsBarOptions : IguiOptions
	{
		public Vector2 size;

		public float value;

		public Color backgroundColor;

		public int colorScheme;

		public float lowValueMax;

		public float mediumValueMax;

		public int boostAttribute;

		public string title;

		public UIStatsBarOptions(string title, Vector2 size, float value, Color backgroundColor, int colorScheme, float lowValueMax, float mediumValueMax, int boostAttribute)
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
