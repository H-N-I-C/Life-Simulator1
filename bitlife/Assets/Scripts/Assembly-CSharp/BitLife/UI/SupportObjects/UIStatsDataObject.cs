using System.Collections.Generic;
using System.Runtime.InteropServices;
using BitLife.UI.Data;
using BitLife.UI.Managers;

namespace BitLife.UI.SupportObjects
{
	public class UIStatsDataObject
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003Ec__DisplayClass9_0
		{
			public StatType types;

			public UIStatsDataObject _003C_003E4__this;
		}

		private List<ScenarioBarGraph> Bars__BackingField;

		private static Dictionary<StatType, string> StatLabels;

		public List<ScenarioBarGraph> Bars
		{
			get
			{
				return Bars__BackingField;
			}
			private set
			{
				Bars__BackingField = value;
			}
		}

		private Life Life => null;

		private LifeInterface LifeInterface => null;

		public UIStatsDataObject GetMainStats(LIFE_EVENT_THEME_NAME lifeEventTheme, out StatType statType, out int numBars)
		{
			statType = default;
			numBars = default;
			return null;
		}

		public UIStatsBarGraphsOptions Create(UIManager gameManager, LIFE_EVENT_THEME_NAME lifeEventTheme)
		{
			return null;
		}

		private void _003CGetMainStats_003Eg__Add_007C9_0(StatType type, float value, BAR_GRAPH_COLOR_SCHEME colorScheme, [Optional][DefaultParameterValue(0)] BOOST_ATTRIBUTE boostAttribute, ref _003C_003Ec__DisplayClass9_0 P_4)
		{
		}
	}
}
