using System;
using System.Collections;

public class ScenarioAstronautTransmit : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDiscovery, bool> _003C_003E9__5_0;

		internal bool _003CCanTransmit_003Eb__5_0(SimDiscovery t)
		{
			return false;
		}
	}

	private readonly SimSpecialJobAstronaut _special;

	private string _narrative;

	public ScenarioAstronautTransmit(SimSpecialJobAstronaut special)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public static bool CanTransmit()
	{
		return false;
	}
}
