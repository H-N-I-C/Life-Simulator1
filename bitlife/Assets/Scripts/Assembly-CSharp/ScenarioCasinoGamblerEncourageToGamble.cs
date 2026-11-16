using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCasinoGamblerEncourageToGamble : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__6_1;

		internal float _003CChoiceSelectedAtIndex_003Eb__6_1(SimCasinoGameRoom t)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public CASINO_GAME_TYPE type;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimCasinoGameRoom t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__2(SimCasinoGameRoom t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGambler _gambler;

	private List<DataCasinoGamblerEncouragementMethod> _dataList;

	public ScenarioCasinoGamblerEncourageToGamble(SimCasinoGambler gambler)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private double GameValueWon(CASINO_GAME_TYPE type, double bet)
	{
		return 0.0;
	}
}
