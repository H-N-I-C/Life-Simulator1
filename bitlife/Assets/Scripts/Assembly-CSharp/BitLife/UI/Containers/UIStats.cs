using System;
using BitLife.UI.CandyObjects;
using BitLife.UI.Data;
using BitLife.UI.Managers;
using BitLife.UI.SupportObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIStats : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<CandyImage> _003C_003E9__38_0;

			internal void _003CCreateBarGraphs_003Eb__38_0(CandyImage i)
			{
			}
		}

		private const float DefaultPreferredHeight = 420f;

		private const float StatsBarGraphsOffsetDefault = -10f;

		private const float StatsBarGraphsOffsetExtra = -20f;

		private UIManager gameManager;

		private UIScenarioManager scenarioManager;

		private Transform statsBarGraphsParent;

		private GameObject dataBlockPrefab;

		private ConstraintScaleBgUI scaleBgUI;

		private GameObject lifeEventOverlayParent;

		private CandyImage prison;

		private CandyImage politics;

		private CandyImage sports;

		private CandyImage streetHustler;

		private CandyImage royalty;

		private CandyImage fame;

		private CandyImage music;

		private CandyImage mafia;

		private CandyImage actor;

		private CandyImage business;

		private CandyImage astronaut;

		private CandyImage dealer;

		private CandyImage vampire;

		private GameObject themeOverlayParent;

		private CandyImage bgOverlay;

		private UIStatsBarGraphs statsBarGraphs;

		private StatType statType;

		private int numBars;

		private void Awake()
		{
		}

		public void Init()
		{
		}

		private void LoadLifeEventThemes()
		{
		}

		public void SetLifeEventTheme(LIFE_EVENT_THEME_NAME lifeEventTheme)
		{
		}

		public bool IsOnTablet()
		{
			return false;
		}

		public void SetToGrayScale()
		{
		}

		public void ShowBoost(int attribute, ScenarioBoost boostScenario)
		{
		}

		public void HideBoosts()
		{
		}

		public void RemoveBoosts()
		{
		}

		public void UpdateStatsForThemeAnd5thBar()
		{
		}

		public void UpdateStats()
		{
		}

		private void CreateBarGraphs(UIStatsBarGraphsOptions options)
		{
		}

		private void LoadSkinThemes()
		{
		}

		public void TranslateBars()
		{
		}
	}
}
