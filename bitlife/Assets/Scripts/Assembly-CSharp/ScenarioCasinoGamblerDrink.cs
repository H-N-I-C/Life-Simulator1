using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCasinoGamblerDrink : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCasinoDrink, string> _003C_003E9__6_0;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__7_1;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__7_2;

		internal string _003CRefreshScenario_003Eb__6_0(DataCasinoDrink t)
		{
			return null;
		}

		internal float _003CChoiceSelectedAtIndex_003Eb__7_1(SimCasinoGameRoom t)
		{
			return 0f;
		}

		internal float _003CChoiceSelectedAtIndex_003Eb__7_2(SimCasinoGameRoom t)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public CASINO_GAME_TYPE favoriteGame;

		internal bool _003CChoiceSelectedAtIndex_003Eb__3(SimCasinoGameRoom t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoGambler _gambler;

	private List<DataCasinoDrink> _drinkList;

	private readonly CASINO_AUDIENCE_TYPE _audience;

	public ScenarioCasinoGamblerDrink(SimCasinoGambler gambler, CASINO_AUDIENCE_TYPE audience)
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

	private bool _003CChoiceSelectedAtIndex_003Eb__7_0(CASINO_GAME_TYPE t)
	{
		return false;
	}
}
