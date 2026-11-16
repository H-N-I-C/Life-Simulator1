using System;

public class ScenarioSecretAgentAgencyProfile : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRivalSpyAgency, string> _003C_003E9__2_0;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__2_1;

		internal string _003CRefreshScenario_003Eb__2_0(SimRivalSpyAgency t)
		{
			return null;
		}

		internal bool _003CRefreshScenario_003Eb__2_1(SimSecretAgentMission t)
		{
			return false;
		}
	}

	private SimSpyAgency _agency;

	public override void RefreshScenario()
	{
	}
}
