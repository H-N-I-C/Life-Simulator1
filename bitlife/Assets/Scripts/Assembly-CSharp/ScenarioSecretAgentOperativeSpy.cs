using System;
using System.Collections;

public class ScenarioSecretAgentOperativeSpy : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(SimSecretAgent x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string operativeName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimSecretAgent x)
		{
			return false;
		}
	}

	private readonly SimPerson _thePerson;

	private SimSecretAgent _theOperative;

	private SimSpyAgency _agency;

	private string _personString;

	private string _narrative;

	public ScenarioSecretAgentOperativeSpy(SimPerson thePerson, string personString = "")
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
