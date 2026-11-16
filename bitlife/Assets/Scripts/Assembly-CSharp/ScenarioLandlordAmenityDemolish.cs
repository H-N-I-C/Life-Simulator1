using System;
using System.Collections;

public class ScenarioLandlordAmenityDemolish : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPropertyRemodel, string> _003C_003E9__2_0;

		internal string _003CRefreshScenario_003Eb__2_0(SimPropertyRemodel t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public ScenarioLandlordAmenityDemolish _003C_003E4__this;

		public string selectedFeature;

		public SimPropertyRemodel remodelItem;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimPropertyRemodel t)
		{
			return false;
		}

		internal void _003CChoiceSelectedAtIndex_003Eb__1(int index)
		{
		}
	}

	private readonly SimHouse _house;

	public ScenarioLandlordAmenityDemolish(SimHouse house)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
