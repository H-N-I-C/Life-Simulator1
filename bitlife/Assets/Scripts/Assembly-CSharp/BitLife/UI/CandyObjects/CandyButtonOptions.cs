using System;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace BitLife.UI.CandyObjects
{
	public class CandyButtonOptions : IguiOptions
	{
		public Action onClickMethod;

		public UnityAction<int> onClickMethodIndexed;

		public Vector2 size;

		public Color color;

		public GameObject displayObjectNormal;

		public GameObject displayObjectPressed;

		public int soundIndex;

		public ScenarioChoice choice;

		public int choiceIndex;

		public string uapLabel;

		public CandyButtonOptions(Action onClickMethod, Vector2 size, Color color, GameObject displayObjectNormal = null, GameObject displayObjectPressed = null, int soundIndex = 0, ScenarioChoice choice = null, UnityAction<int> onClickMethodIndexed = null, int choiceIndex = -1, string uapLabel = null)
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
