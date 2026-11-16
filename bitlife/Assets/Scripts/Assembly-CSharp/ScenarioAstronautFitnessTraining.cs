using System;
using System.Collections;

public class ScenarioAstronautFitnessTraining : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<char, bool> _003C_003E9__5_0;

		internal bool _003CRefreshScenario_003Eb__5_0(char t)
		{
			return false;
		}
	}

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly string _countryAdjectival;

	private string _agencyTypeString;

	private bool _isAcademy;

	public ScenarioAstronautFitnessTraining(SimSpecialJobAstronaut specialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
