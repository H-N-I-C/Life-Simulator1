using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioVampireCouncilParty : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataVampireCouncilPartyType, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(DataVampireCouncilPartyType q)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public string partyTypeString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataVampireCouncilPartyType q)
		{
			return false;
		}
	}

	private List<DataVampireCouncilPartyType> _councilParties;

	private CouncilPartyActivity _councilPartyActivity;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void PushFeedbackScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
