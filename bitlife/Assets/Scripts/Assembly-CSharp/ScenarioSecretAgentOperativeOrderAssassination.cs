using System;
using System.Collections;
using Game;

public class ScenarioSecretAgentOperativeOrderAssassination : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataSecretAgentAssassinationMethod, string> _003C_003E9__4_0;

		public static Func<string, string> _003C_003E9__4_1;

		internal string _003CRefreshScenario_003Eb__4_0(DataSecretAgentAssassinationMethod x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__4_1(string t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	public ScenarioSecretAgentOperativeOrderAssassination(SimSecretAgent agent)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
