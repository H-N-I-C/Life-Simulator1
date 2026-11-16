using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioSecretAgentInfiltrateChoose : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRivalSpyAgency, SECRET_AGENT_RIVAL_AGENCY_SECURITY_LEVEL> _003C_003E9__5_0;

		internal SECRET_AGENT_RIVAL_AGENCY_SECURITY_LEVEL _003CRefreshScenario_003Eb__5_0(SimRivalSpyAgency t)
		{
			return SECRET_AGENT_RIVAL_AGENCY_SECURITY_LEVEL.NONE;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private ArrayList _dropDownArray;

	private List<SimRivalSpyAgency> _orderedList;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
