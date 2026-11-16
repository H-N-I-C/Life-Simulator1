using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.SupportObjects
{
	public class UIStatsBarGraphsOptions : IguiOptions
	{
		public List<UIStatsBar> bars;

		public int lifeEventTheme;

		public UIStatsBarGraphsOptions(List<UIStatsBar> bars, int lifeEventTheme)
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
