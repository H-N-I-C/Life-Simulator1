using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class MiniGameOptions : IguiOptions
	{
		public float width;

		public Scenario scenario;

		public MiniGameOptions(float width, Scenario scenario)
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
