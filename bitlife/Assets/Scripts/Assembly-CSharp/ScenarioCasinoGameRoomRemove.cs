using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoGameRoomRemove : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGameRoom, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(SimCasinoGameRoom t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private List<SimCasinoGameRoom> _gameRooms;

	private ArrayList _dropDownContents;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
