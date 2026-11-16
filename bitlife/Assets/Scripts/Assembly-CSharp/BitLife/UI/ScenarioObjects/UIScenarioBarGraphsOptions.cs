using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.ScenarioObjects
{
	public class UIScenarioBarGraphsOptions : IguiOptions
	{
		public IguiObject[] titles;

		public IguiObject[] graphs;

		public UIScenarioBarGraphsOptions(IguiObject[] titles, IguiObject[] graphs)
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
