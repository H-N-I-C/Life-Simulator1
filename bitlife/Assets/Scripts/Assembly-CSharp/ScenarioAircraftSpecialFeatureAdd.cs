using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioAircraftSpecialFeatureAdd : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataLuxuryLuxuryAircraftSpecialFeature t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimAircraft _aircraft;

	private readonly bool _newlyPurchasedAircraft;

	private List<DataLuxuryLuxuryAircraftSpecialFeature> _listFeatures;

	public ScenarioAircraftSpecialFeatureAdd(SimAircraft aircraft, bool newlyPurchasedAircraft = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
