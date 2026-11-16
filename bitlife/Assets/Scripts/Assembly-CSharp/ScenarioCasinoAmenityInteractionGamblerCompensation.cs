using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCasinoAmenityInteractionGamblerCompensation : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCasinoGamblerCompensationMethod, string> _003C_003E9__5_0;

		public static Func<DataCasinoGamblerCompensationMethod, string> _003C_003E9__5_1;

		internal string _003CRefreshScenario_003Eb__5_0(DataCasinoGamblerCompensationMethod t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_1(DataCasinoGamblerCompensationMethod t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataCasinoGamblerCompensationMethod t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGambler _gambler;

	private List<DataCasinoGamblerCompensationMethod> _compensationMethods;

	public ScenarioCasinoAmenityInteractionGamblerCompensation(SimCasinoGambler gambler)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
