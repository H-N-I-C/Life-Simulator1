using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooHabitatFeatureAdd : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataZooTradeHabitatFeature t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooHabitat _habitat;

	private List<DataZooTradeHabitatFeature> _dataList;

	public ScenarioZooHabitatFeatureAdd(SimZooHabitat habitat)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
