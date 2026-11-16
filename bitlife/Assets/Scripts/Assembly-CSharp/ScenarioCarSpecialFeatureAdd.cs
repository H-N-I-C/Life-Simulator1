using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCarSpecialFeatureAdd : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataLuxuryLuxuryCarSpecialFeature t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimCar _car;

	private readonly bool _newlyPurchasedCar;

	private List<DataLuxuryLuxuryCarSpecialFeature> _listFeatures;

	public ScenarioCarSpecialFeatureAdd(SimCar car, bool newlyPurchasedCar = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
