using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.ScenarioObjects
{
	public class UIScenarioBarGraphs : MonoBehaviour, IguiObject
	{
		private const float SIDE_MARGIN = 30f;

		private GameObject titleColumnParent;

		private GameObject barGraphColumnParent;

		private GameObject uapColumnParent;

		private GameObject shadow;

		private List<CandyBarGraph> barGraphs;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void SetupTitleTranslations()
		{
		}

		public void SetGravestoneBarValues(float happinessValue, float karmaValue)
		{
		}

		public void TurnOffShadow()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}
	}
}
