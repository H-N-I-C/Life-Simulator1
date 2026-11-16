using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingDriverCashRacePickCar : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCar, string> _003C_003E9__7_0;

		public static Func<SimRaceCar, string> _003C_003E9__7_1;

		internal string _003CRefreshScenario_003Eb__7_0(SimRaceCar x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__7_1(SimRaceCar x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string selectedChoice;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimRaceCar t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimRaceCarDriver _driver;

	private readonly float _cashAmount;

	private SimRacing _racing;

	private readonly string _trackName;

	private List<SimRaceCar> _orderedList;

	public ScenarioRacingDriverCashRacePickCar(SimRaceCarDriver driver, float cashAmount, string trackName)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
